using AutoMapper;
using MusicSystem.Data.Models;
using MusicSystem.Repositories.Interfaces;
using MusicSystem.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicSystem.Services
{
    public class AlbumsService : IAlbumsServicec
    {
        private readonly IRepository<Album> repository;
        private readonly IMapper mapper;
        public AlbumsService(IRepository<Album> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<bool> DeleteByProducerId(int id)
        {
            var albums = this.repository.All()
                .Where(x => x.Id == id);

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
    }
}
