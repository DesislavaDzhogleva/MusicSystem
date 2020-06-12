using AutoMapper;
using MusicSystem.Data.Models;
using MusicSystem.DTOs;
using MusicSystem.Repositories.Interfaces;
using MusicSystem.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicSystem.Services
{
    public class AlbumsService : IAlbumsService
    {
        private readonly IRepository<Album> repository;
        private readonly IMapper mapper;
        public AlbumsService(IRepository<Album> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public IEnumerable<T> GetAll<T>()
        {
            var album = this.repository.All();

            var resultDto = this.mapper.Map<List<T>>(album);
            return resultDto;
        }

        public T GetById<T>(int id)
        {
            var album = this.repository.All()
                .Where(x => x.Id == id)
                .FirstOrDefault();

            var resultDto = this.mapper.Map<T>(album);
            return resultDto;
        }

        public IEnumerable<T> GetByName<T>(string name)
        {
            var album = this.repository.All()
                .Where(x => x.Name == name);

            var resultDto = this.mapper.Map<List<T>>(album);
            return resultDto;
        }


        public async Task<int> AddAsync(AlbumDto input)
        {
            var existsWithName = this.repository.All()
                .Where(x => x.Name == input.Name)
                .FirstOrDefault();

            if (existsWithName != null)
            {
                return -1;
            }

            var album = new Album()
            {
               Name = input.Name,
               ReleaseDate = input.ReleaseDate,
               ProducerId = input.ProducerId
            };

            await this.repository.AddAsync(album);
            await this.repository.SaveChangesAsync();

            //var albumReuslt = this.repository.All()
            //    .Where(x => x.Name == album.Name)
            //    .FirstOrDefault();

            return album.Id;
        }

        
       
        public async Task<bool> Update(int id, AlbumDto albumDto)
        {
            var album = this.repository.All()
                .Where(x => x.Id == id)
                .FirstOrDefault();

            if ( album == null)
            {
                return false;
            }

            this.mapper.Map<AlbumDto, Album>(albumDto, album);
            album.Id = id;


            this.repository.Update(album);
            await this.repository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteByProducerId(int id)
        {
            var albums = this.repository.All()
                .Where(x => x.ProducerId == id);

            if (albums != null)
            {
                foreach (var album in albums)
                {
                    album.ProducerId = null;
                }
                await this.repository.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> Delete(int id)
        {
            var album = this.repository.All()
                 .Where(x => x.Id == id)
                 .FirstOrDefault();

            if (album != null)
            {
                this.repository.Delete(album);
                await this.repository.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public bool Exists(int id)
        {
            var album = this.repository.All()
               .FirstOrDefault(x => x.Id == id);

            if (album == null)
            {
                return false;
            }

            return true;
        }

        public bool IsUnique(string name, int id)
        {
            var existsWithName = this.repository.All()
              .Where(x => x.Name == name && x.Id != id)
              .FirstOrDefault();

            if (existsWithName == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
