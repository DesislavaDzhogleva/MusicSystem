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
    public class SongsService : ISongsService
    {
        private readonly IRepository<Song> repository;
        private readonly IMapper mapper;

        public SongsService(IRepository<Song> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public IEnumerable<T> GetAll<T>()
        {
            var songs = this.repository.All();

            var resultDto = this.mapper.Map<List<T>>(songs);
            return resultDto;
        }

        public T GetById<T>(int id)
        {
            var song = this.repository.All()
                .Where(x => x.Id == id)
                .FirstOrDefault();

            var resultDto = this.mapper.Map<T>(song);
            return resultDto;
        }

        public async Task<int> Add(SongDto input)
        {
            var existsWithName = this.repository.All()
               .Where(x => x.Name == input.Name)
               .FirstOrDefault();

            if (existsWithName != null)
            {
                return -1;
            }

            var song = new Song()
            {
                Name = input.Name,
                Genre = input.Genre,
                WriterId = (int)input.WriterId,
                AlbumId = (int)input.AlbumId,
                Price = input.Price,
                Duration = input.Duration,
                CreatedOn = input.CreatedOn
            };

            await this.repository.AddAsync(song);
            await this.repository.SaveChangesAsync();

            var id = this.repository.All()
                .Where(x => x.Name == song.Name)
                .FirstOrDefault().Id;

            return id;
        }

        public async Task<bool> Update(int id, SongDto songDto)
        {
            var song = this.repository.All()
                .Where(x => x.Id == id)
                .FirstOrDefault();

            if (song == null)
            {
                return false;
            }

            this.mapper.Map<SongDto, Song>(songDto, song);
            song.Id = id;


            this.repository.Update(song);
            await this.repository.SaveChangesAsync();
            return true;
        }


        public async Task<bool> DeleteBySongId(int id)
        {
            var song = this.repository.All()
                .Where(x => x.Id == id)
                .FirstOrDefault();

            if (song != null)
            {
                this.repository.Delete(song);
                await this.repository.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteByAlbumId(int id)
        {
            var songs = this.repository.All()
                .Where(x => x.AlbumId == id);

            if (songs != null)
            {
                foreach(var song in songs)
                {
                    this.repository.Delete(song);
                }
                await this.repository.SaveChangesAsync();
                return true;
            }

            return false;
        }
        
        public async Task<bool> DeleteByWriterId(int id)
        {
            var songs = this.repository.All()
                .Where(x => x.WriterId == id);

            if (songs != null)
            {
                foreach(var song in songs)
                {
                    this.repository.Delete(song);
                }
                await this.repository.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public bool Exists(int id)
        {
            var song = this.repository.All()
                .FirstOrDefault(x => x.Id == id);

            if(song == null)
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
