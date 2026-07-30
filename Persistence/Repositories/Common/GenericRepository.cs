using Application.Abstractions.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Repositories.Common
{
    public abstract class GenericRepository<TContext, TEntity>:ReadRepository<TContext, TEntity>,IGenericRepository<TEntity>
    where TContext : IDbContext
    where TEntity : class
    {
        protected GenericRepository(TContext context)
            : base(context)
        {
        }

        public async Task AddAsync(TEntity entity)
        {
           await _context.AddEntity(entity);
        }

        public void Update(TEntity entity)
        {
            _context.UpdateEntity(entity);
        }

        public void Remove(TEntity entity)
        {
            _context.RemoveEntity(entity);
        }
    }
}

