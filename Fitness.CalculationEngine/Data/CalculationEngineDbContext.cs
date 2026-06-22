using Microsoft.EntityFrameworkCore;

namespace Fitness.CalculationEngine.Data;

public class CalculationEngineDbContext:DbContext
{
    public CalculationEngineDbContext(DbContextOptions<CalculationEngineDbContext> options)
        : base(options)
    {
        
    }
}
