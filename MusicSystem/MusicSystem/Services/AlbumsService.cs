using AutoMapper;
using MusicSystem.Data.Models;
using MusicSystem.DTOs;
using MusicSystem.Repositories.Interfaces;
using MusicSystem.Services.Interfaces;
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


        public async Task<int> Add(AlbumDto input)
        {
            var producer = new Album()
            {
               Name = input.Name,
               ReleaseDate = input.ReleaseDate,
               ProducerId = input.ProducerId
            };

            await this.repository.AddAsync(producer);
            await this.repository.SaveChangesAsync();

            return 1;
        }

        public async Task<bool> DeleteByProducerId(int id)
        {
            var albums = this.repository.All()
                .Where(x => x.ProducerId == id);

            if (albums != null)
            {
                foreach(var album in albums)
                {
                    album.ProducerId = null;
                }
                await this.repository.SaveChangesAsync();
                return true;
            }

            return false;
        }
       
        public async Task<bool> Update(int id, AlbumDto albumDto)
        {
            var album = this.repository.All()
                .Where(x => x.Id == id)
                .FirstOrDefault();

            if (album == null)
            {
                return false;
            }

            this.mapper.Map<AlbumDto, Album>(albumDto, album);
            album.Id = id;


            this.repository.Update(album);
            await this.repository.SaveChangesAsync();
            return true;
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
    }
}
