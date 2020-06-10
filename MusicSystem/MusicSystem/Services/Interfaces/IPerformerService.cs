using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicSystem.Services.Interfaces
{
    public interface IPerformerService
    {
        bool Exists(int id);
    }
}
