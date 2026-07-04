using Fitness.CalculationEngine.Domain.Entities;
using Fitness.CalculationEngine.Domain.Enums;
using Fitness.CalculationEngine.Infrastructure.Persistence.Repositories;
using Fitness.CalculationEngine.Shared.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Fitness.CalculationEngine.Features.UserFitnessStats.GetUserFitnessGoal;

public record GetUserFitnessGoalQuery
(string UserId) : IRequest<RequestResult<FitnessGoal>>;

public class GetUserFitnessGoalQueryHandler(Repository<UserFitnessStat> repository) : IRequestHandler<GetUserFitnessGoalQuery, RequestResult<FitnessGoal>>
{
    private readonly Repository<UserFitnessStat> _repository = repository;

    public async Task<RequestResult<FitnessGoal>> Handle(GetUserFitnessGoalQuery request, CancellationToken cancellationToken)
    {

        var fitnessGoal = await _repository.Get(s => s.UserId == request.UserId)
                                           .Select(s => s.Goal)
                                           .FirstOrDefaultAsync(cancellationToken);

        if (fitnessGoal == 0)
            return RequestResult<FitnessGoal>.Failure(ResultCode.UserFitnessStatsNotFound);
  
        return RequestResult<FitnessGoal>.succeeded(fitnessGoal, ResultCode.FitnessGoalRetrieved);
    }
 
}
