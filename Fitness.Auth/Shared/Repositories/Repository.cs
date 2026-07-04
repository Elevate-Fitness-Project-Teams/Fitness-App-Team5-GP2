using Fitness.Auth.Infrastructure.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace Fitness.Auth.Shared.Repositories;

public class Repository<T>
    where T : class
{
    private readonly AuthDbContext _authDbContext;
    private readonly DbSet<T> _entities;

    public Repository(AuthDbContext authDbContext)
    {
        _authDbContext = authDbContext;
        _entities = _authDbContext.Set<T>();
    }
    public IQueryable<T> Get()
        => _entities.AsNoTracking();

    public IQueryable<T> Get(Expression<Func<T, bool>> predicate)
        => _entities.Where(predicate).AsNoTracking();

    public void Add(T entity)
    {
        _entities.Add(entity);
    }
    public async Task<int> SaveChangeAsync(CancellationToken cancellationToken)
    {
        return await _authDbContext.SaveChangesAsync();
    }
    
    public Task<int> BulkUpdateAsync<TProp>(
      Expression<Func<T, bool>> predicate,
     Func<T, TProp> updateProp,
      TProp newValue,
      CancellationToken cancellationToken = default)
    {
        return _entities
            .Where(predicate)
            .ExecuteUpdateAsync(
                setters => setters.SetProperty(updateProp, newValue),
                cancellationToken
            );
    }
}