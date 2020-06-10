using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicSystem.Repositories.Interfaces
{
    public interface IRepository<TEntity>
    {
        IQueryable<TEntity> All();

        Task AddAsync(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        Task<int> SaveChangesAsync();
    }
}
