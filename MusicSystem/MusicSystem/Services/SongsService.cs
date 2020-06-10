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

        public async Task<int> Add(SongDto input)
        {
            var song = new Song()
            {
                Name = input.Name,
                Genre = input.Genre,
                WriterId = input.WriterId,
                AlbumId = input.AlbumId,
                Price = input.Price,
                Duration = input.Duration,
                CreatedOn = input.CreatedOn,
                Performers = input.Performers
            };

            await this.repository.AddAsync(song);
            await this.repository.SaveChangesAsync();

            return song.Id;
        }

        public bool DeleteBySongAndPerformerId(int songId, int performerId)
        {
            throw new NotImplementedException();
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

        public async Task<bool> Update(int id, SongDto songDto)
        {
            var song = this.repository.All()
                .Where(x => x.Id == id)
                .FirstOrDefault();
            if (song == null)
            {
                return false;
            }

            this.mapper.Map<SongDto,Song>(songDto,song);
            song.Id = id;
   

            this.repository.Update(song);
            await this.repository.SaveChangesAsync();
            return true;
        }

    }
}
