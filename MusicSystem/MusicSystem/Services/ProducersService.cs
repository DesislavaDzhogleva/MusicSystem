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
    public class ProducersService : IProducersService
    {
        private readonly IRepository<Producer> repository;
        private readonly IMapper mapper;

        public ProducersService(IRepository<Producer> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public IEnumerable<T> GetAll<T>()
        {
            var producers = this.repository.All();

            var resultDto = this.mapper.Map<List<T>>(producers);
            return resultDto;
        }

        public T GetById<T>(int id)
        {
            var producer = this.repository.All()
                .Where(x => x.Id == id)
                .FirstOrDefault();

            var resultDto = this.mapper.Map<T>(producer);
            return resultDto;
        }

        public async Task<int> Add(ProducerDto input)
        {
            var existsWithName = this.repository.All()
               .Where(x => x.Name == input.Name)
               .FirstOrDefault();

            if (existsWithName != null)
            {
                return -1;
            }

            var producer = new Producer()
            {
                Name = input.Name,
                Pseudonym = input.Pseudonym,
                Email = input.Email,
                PhoneNumber = input.PhoneNumber
            };

            await this.repository.AddAsync(producer);
            await this.repository.SaveChangesAsync();

            var id = this.repository.All()
                .Where(x => x.Name == producer.Name)
                .FirstOrDefault().Id;

            return id;
        }

        public async Task<bool> Update(int id, ProducerDto producerDto)
        {
            var producer = this.repository.All()
                .Where(x => x.Id == id)
                .FirstOrDefault();

            if (producer == null)
            {
                return false;
            }

            this.mapper.Map<ProducerDto, Producer>(producerDto, producer);
            producer.Id = id;


            this.repository.Update(producer);
            await this.repository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var producer = this.repository.All()
                .Where(x => x.Id == id)
                .FirstOrDefault();

            if (producer != null)
            {
                this.repository.Delete(producer);
                await this.repository.SaveChangesAsync();
                return true;
            }

            return false;
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
