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
    public class WritersService : IWritersService
    {
        private readonly IRepository<Album> repository;
        private readonly IMapper mapper;
        public WritersService(IRepository<Album> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
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
