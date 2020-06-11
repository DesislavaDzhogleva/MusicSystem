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

        Task<int> Add(WriterDto input);

        Task<bool> Delete(int id);

        Task<bool> Update(int id, WriterDto song);

        T GetById<T>(int id);
        bool Exists(int id);
    }
}
