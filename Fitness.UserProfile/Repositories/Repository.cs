using Fitness.UserProfile.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace Fitness.UserProfile.Repositories;

public class Repository<T>
    where T: class
{
    private readonly UserProfileDbContext _userProfileDbContext;
    private readonly DbSet<T> _entities;

    public Repository(UserProfileDbContext userProfileDbContext)
    {
        _userProfileDbContext = userProfileDbContext;
        _entities = _userProfileDbContext.Set<T>();
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
        return await _userProfileDbContext.SaveChangesAsync();
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
