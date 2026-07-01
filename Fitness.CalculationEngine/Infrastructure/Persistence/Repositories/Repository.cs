using Fitness.CalculationEngine.Domain.Entities;
using Fitness.CalculationEngine.Infrastructure.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

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
}
