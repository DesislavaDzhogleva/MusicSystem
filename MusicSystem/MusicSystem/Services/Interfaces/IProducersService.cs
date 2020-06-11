using MusicSystem.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicSystem.Services.Interfaces
{
    public interface IProducersService
    {
        IEnumerable<T> GetAll<T>();

        Task<int> Add(ProducerDto input);

        Task<bool> Delete(int id);

        Task<bool> Update(int id, ProducerDto song);

        T GetById<T>(int id);

        bool Exists(int id);

        bool IsUnique(string name, int id);
    }
}
