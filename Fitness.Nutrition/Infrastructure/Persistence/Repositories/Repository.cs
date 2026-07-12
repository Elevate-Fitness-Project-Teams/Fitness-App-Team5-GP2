using Fitness.Nutrition.Domain.Entities;
using Fitness.Nutrition.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace Fitness.Nutrition.Infrastructure.Persistence.Repositories;

public class Repository<T> 
    where T : BaseEntity
{
    private readonly NutritionDbContext _nutritionDbContext;
    private readonly DbSet<T> _entities;

    public Repository(NutritionDbContext nutritionDbContext)
    {
        _nutritionDbContext = nutritionDbContext;
        _entities = _nutritionDbContext.Set<T>();
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
        return await _nutritionDbContext.SaveChangesAsync();
    }
    public void SaveInclude(T entity, params string[] includedProperties)
    {
        var localEntity = _entities.Local.FirstOrDefault(e => e.Id == entity.Id);
        EntityEntry entry;
        if (localEntity == null)
        {
            _entities.Attach(entity);
            entry = _nutritionDbContext.Entry(entity);
        }
        else
        {
            entry = _nutritionDbContext.Entry(localEntity);
            _nutritionDbContext.Entry(localEntity).CurrentValues.SetValues(entity);
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
