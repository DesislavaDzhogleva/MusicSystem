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
    public class WritersService : IWritersService
    {
        private readonly IRepository<Writer> repository;
        private readonly IMapper mapper;
        public WritersService(IRepository<Writer> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public IEnumerable<T> GetAll<T>()
        {
            var writers = this.repository.All();

            var resultDto = this.mapper.Map<List<T>>(writers);
            return resultDto;
        }

        public T GetById<T>(int id)
        {
            var writers = this.repository.All()
                .Where(x => x.Id == id)
                .FirstOrDefault();

            var resultDto = this.mapper.Map<T>(writers);
            return resultDto;
        }

        public async Task<int> Add(WriterDto input)
        {
            var writer = new Writer()
            {
                Name = input.Name,
                Pseudonym = input.Pseudonym,
                DateOfBirth = input.DateOfBirth,
            };

            await this.repository.AddAsync(writer);
            await this.repository.SaveChangesAsync();

            return writer.Id;
        }

        public async Task<bool> Update(int id, WriterDto writerDto)
        {
            var writer = this.repository.All()
                .Where(x => x.Id == id)
                .FirstOrDefault();

            if (writer == null)
            {
                return false;
            }

            this.mapper.Map<WriterDto, Writer>(writerDto, writer);
            writer.Id = id;


            this.repository.Update(writer);
            await this.repository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var writer = this.repository.All()
                .Where(x => x.Id == id)
                .FirstOrDefault();

            if (writer != null)
            {
                this.repository.Delete(writer);
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
