using MusicSystem.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicSystem.Services.Interfaces
{
    public interface ISongsPerformersService
    {
        Task<int> Add(SongPerformerDto input);

        IEnumerable<T> GetAll<T>();

        T GetById<T>(int id);

        bool Exists(int id);

        IEnumerable<T> GetBySongId<T>(int songId);

        Task<bool> DeleteAsync(int id);

        Task<bool> DeleteBySongIdAsync(int songId);

        Task<bool> Update(int id, SongPerformerDto songPerformerDto);

    }
}
