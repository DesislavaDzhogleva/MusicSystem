using MusicSystem.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicSystem.Repositories
{
    using Interfaces;
    using Microsoft.EntityFrameworkCore;
    using MusicSystem.Data;

    public class EfRepository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        public EfRepository(ApplicationDbContext context)
        {
            this.Context = context ?? throw new ArgumentNullException(nameof(context));
            this.DbSet = this.Context.Set<TEntity>();
        }

        protected DbSet<TEntity> DbSet { get; set; }

        protected ApplicationDbContext Context { get; set; }

        public Task AddAsync(TEntity entity) => this.DbSet.AddAsync(entity).AsTask();

        public IQueryable<TEntity> All() => this.DbSet;

        public void Delete(TEntity entity) => this.DbSet.Remove(entity);

        public Task<int> SaveChangesAsync() => this.Context.SaveChangesAsync();

        public void Update(TEntity entity) => this.Context.Update(entity);
    }
}
