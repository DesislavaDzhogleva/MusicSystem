using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicSystem.Services.Interfaces
{
    public interface ISongsPerformersService
    {
        IEnumerable<T> GetAll<T>();

        T GetById<T>(int id);

        bool Exists(int songId, int performerId);

        IEnumerable<T> GetBySongId<T>(int songId);

        Task<bool> DeleteAsync(int id);

        Task<bool> DeleteBySongIdAsync(int songId);
    }
}
