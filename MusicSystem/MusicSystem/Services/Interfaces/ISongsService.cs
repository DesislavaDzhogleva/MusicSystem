﻿using MusicSystem.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicSystem.Services.Interfaces
{
    public interface ISongsService
    {
        IEnumerable<T> GetAll<T>();

        IEnumerable<T> GetByAlbumName<T>(string albumName);

        IEnumerable<T> GetByWriterName<T>(string writerName);

        IEnumerable<T> GetByName<T>(string name);

        Task<int> Add(SongDto input);

        Task<bool> DeleteBySongId(int id);

        Task<bool> DeleteByAlbumId(int id);

        Task<bool> DeleteByWriterId(int id);

        Task<bool> Update(int id, SongDto song);

        T GetById<T>(int id);

        bool Exists(int id);

        bool IsUnique(string name, int id);
    }
}
