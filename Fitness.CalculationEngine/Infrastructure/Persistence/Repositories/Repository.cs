using Fitness.CalculationEngine.Domain.Entities;
using Fitness.CalculationEngine.Infrastructure.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace Fitness.CalculationEngine.Infrastructure.Persistence.Repositories;

public class Repository<T> 
    where T : BaseEntity
{
    private readonly CalculationEngineDbContext _calculationEngineDbContext;
    private readonly DbSet<T> _entities;

    public Repository(CalculationEngineDbContext calculationEngineDbContext)
    {
        _calculationEngineDbContext = calculationEngineDbContext;
        _entities = _calculationEngineDbContext.Set<T>();
    }
    public IQueryable<T> Get()
        => _entities.AsNoTracking();

    public IQueryable<T> Get(Expression<Func<T, bool>> predicate)
        => _entities.Where(predicate).AsNoTracking();

    public void Add(T entity)
    {
        entity.Id = Guid.NewGuid();
        entity.CreatedAt = DateTime.Now;
        entity.CreatedBy = "";
        _entities.Add(entity);
    }
    public async Task<int> SaveChangeAsync(CancellationToken cancellationToken)
    {
        return await _calculationEngineDbContext.SaveChangesAsync();
    }
    public void SaveInclude(T entity, params string[] includedProperties)
    {
        var localEntity = _entities.Local.FirstOrDefault(e => e.Id == entity.Id);
        EntityEntry entry;
        if (localEntity == null)
        {
            _entities.Attach(entity);
            entry = _calculationEngineDbContext.Entry(entity);
        }
        else
        {
            entry = _calculationEngineDbContext.Entry(localEntity);
            _calculationEngineDbContext.Entry(localEntity).CurrentValues.SetValues(entity);
        }
        foreach (var property in entry.Properties)
        {
            if (property.Metadata.IsPrimaryKey())
                continue;
            property.IsModified = includedProperties.Contains(property.Metadata.Name);
        }
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
