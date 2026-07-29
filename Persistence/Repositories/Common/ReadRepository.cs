using Application.Abstractions.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Persistence.Repositories.Common { 
public abstract class ReadRepository<TContext, TEntity>
    : IReadRepository<TEntity>
    where TContext : IDbContext
    where TEntity : class
{
    protected readonly TContext _context;

    protected ReadRepository(TContext context)
    {
        _context = context;
    }

    public IQueryable<TEntity> GetAll(bool asNoTracking = true)
    {
        return _context.Query<TEntity>(asNoTracking);
    }

    public async Task<TEntity?> FirstOrDefaultAsync(
        Expression<Func<TEntity, bool>> predicate,
        bool asNoTracking = true,
        CancellationToken cancellationToken = default)
    {
        return await GetAll(asNoTracking)
            .FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public async Task<bool> ExistsAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return await GetAll()
            .AnyAsync(predicate, cancellationToken);
    }

    public async Task<int> CountAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = GetAll();

        if (predicate != null)
            query = query.Where(predicate);

        return await query.CountAsync(cancellationToken);
    }
} }
