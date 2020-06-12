using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicSystem.Services.Interfaces
{
    public interface IPerformerService
    {
        IEnumerable<T> GetByName<T>(string name);
        bool Exists(int id);
    }
}
