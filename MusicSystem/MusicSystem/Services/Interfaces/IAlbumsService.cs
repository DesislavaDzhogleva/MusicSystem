using MusicSystem.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicSystem.Services.Interfaces
{
    public interface IAlbumsService
    {
        IEnumerable<T> GetAll<T>();

        T GetById<T>(int id);

        Task<int> AddAsync(AlbumDto input);

        Task<bool> Update(int id, AlbumDto album);

        Task<bool> DeleteByProducerId(int id);

        Task<bool> Delete(int id);

        bool Exists(int id);

        bool IsUnique(string name, int id);
    }
}
