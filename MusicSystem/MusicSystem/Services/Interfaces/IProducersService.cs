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

        T GetById<T>(int id);

        IEnumerable<T> GetByPseudonym<T>(string name);

        Task<int> Add(ProducerDto input);

        Task<bool> Delete(int id);

        Task<bool> Update(int id, ProducerDto song);

        

        bool Exists(int id);

        bool IsUnique(string name, int id);
    }
}
