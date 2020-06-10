﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicSystem.Services.Interfaces
{
    public interface IAlbumsServicec
    {
        Task<bool> DeleteByProducerId(int id);

        bool Exists(int id);
    }
}
