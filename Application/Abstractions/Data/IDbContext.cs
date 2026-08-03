using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Abstractions.Data
{
    public interface IDbContext
    {
        DbSet<TEntity> GetDbSet<TEntity>()
           where TEntity : class;

        IQueryable<TEntity> Query<TEntity>(bool asNoTracking = true)
            where TEntity : class;

        void AddEntity<TEntity>(TEntity entity)
            where TEntity : class;

        void UpdateEntity<TEntity>(TEntity entity)
            where TEntity : class;

        void RemoveEntity<TEntity>(TEntity entity)
            where TEntity : class;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
