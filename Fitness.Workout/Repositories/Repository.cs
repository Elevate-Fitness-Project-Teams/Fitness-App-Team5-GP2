
using Fitness.Workout.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace Fitness.Workout.Infrastructure.Persistence.Repositories;

public class Repository<T> 
    where T : class
{
    private readonly WorkoutDbContext _workoutDbContext;
    private readonly DbSet<T> _entities;

    public Repository(WorkoutDbContext calculationEngineDbContext)
    {
        _workoutDbContext = calculationEngineDbContext;
        _entities = _workoutDbContext.Set<T>();
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
        return await _workoutDbContext.SaveChangesAsync();
    }

    public void Update(T entity)
    {
        _entities.Update(entity);
    }
   
}
