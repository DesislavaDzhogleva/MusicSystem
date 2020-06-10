﻿using AutoMapper;
using MusicSystem.Data.Models;
using MusicSystem.Repositories.Interfaces;
using MusicSystem.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicSystem.Services
{
    public class SongsPerformersService : ISongsPerformersService
    {
        private readonly IRepository<SongPerformer> repository;
        private readonly IMapper mapper;

        public SongsPerformersService(IRepository<SongPerformer> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public IEnumerable<T> GetBySongId<T>(int songId)
        {
            var songs = this.repository.All()
                .Where(x => x.SongId == songId);

            var resultDtos = this.mapper.Map<List<T>>(songs);
            return resultDtos;
        }

        public T GetById<T>(int id)
        {
            var song = this.repository.All()
                .Where(x => x.Id == id)
                .FirstOrDefault();

            var resultDto = this.mapper.Map<T>(song);
            return resultDto;
        }

        public IEnumerable<T> GetAll<T>()
        {
            var songs = this.repository.All();

            var resultDto = this.mapper.Map<List<T>>(songs);
            return resultDto;
        }

        public bool Exists(int songId, int performerId)
        {
            var song = this.repository.All()
                .FirstOrDefault(x => x.SongId == songId && x.PerformerId == performerId);

            if (song == null)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var song = this.repository.All()
                .FirstOrDefault(x => x.Id == id);

            if (song != null)
            {
                this.repository.Delete(song);
                await this.repository.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteBySongIdAsync(int songId)
        {
            var songs = this.repository.All()
                .Where(x => x.SongId == songId);

            if (songs.Count() > 0)
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
    }
}
