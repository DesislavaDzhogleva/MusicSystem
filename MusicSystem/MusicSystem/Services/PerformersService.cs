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
    public class PerformersService : IPerformerService
    {
        private readonly IRepository<Performer> repository;
        private readonly IMapper mapper;

        public PerformersService(IRepository<Performer> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public bool Exists(int id)
        {
            var performer = this.repository.All()
               .FirstOrDefault(x => x.Id == id);

            if (performer == null)
            {
                return false;
            }

            return true;
        }
    }
}
