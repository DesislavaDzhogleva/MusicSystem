using MusicSystem.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicSystem.Services.Interfaces
{
    public interface IWritersService
    {
        IEnumerable<T> GetAll<T>();

        T GetById<T>(int id);

        IEnumerable<T> GetByName<T>(string name);

        Task<int> Add(WriterDto input);

        Task<bool> Delete(int id);

        Task<bool> Update(int id, WriterDto song);

        
        bool Exists(int id);
    }
}
