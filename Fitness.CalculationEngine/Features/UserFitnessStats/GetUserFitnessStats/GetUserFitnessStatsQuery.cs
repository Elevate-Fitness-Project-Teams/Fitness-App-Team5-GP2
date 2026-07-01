using Fitness.CalculationEngine.Domain.Entities;
using Fitness.CalculationEngine.Domain.Enums;
using Fitness.CalculationEngine.Infrastructure.Persistence.Repositories;
using Fitness.CalculationEngine.Shared.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Fitness.CalculationEngine.Features.UserFitnessStats.GetUserFitnessStats;

public record GetUserFitnessStatsQuery
(string UserId) : IRequest<RequestResult<GetUserFitnessStatsResponse>>;

public record GetUserFitnessStatsResponse(double Weight, double Height, int Age, Gender Gender
    , FitnessGoal Goal, ActivityLevel ActivityLevel);


public class GetUserFitnessStatsQueryHandler(Repository<UserFitnessStat> repository) : IRequestHandler<GetUserFitnessStatsQuery, RequestResult<GetUserFitnessStatsResponse>>
{
    private readonly Repository<UserFitnessStat> _repository = repository;

    public async Task<RequestResult<GetUserFitnessStatsResponse>> Handle(GetUserFitnessStatsQuery request, CancellationToken cancellationToken)
    {
        var userStats = await _repository.Get()
                                   .Where(s => s.UserId == request.UserId)
                                   .Select(s => new GetUserFitnessStatsResponse
                                       (s.Weight,s.Height,s.Age,s.Gender,s.Goal,s.ActivityLevel)
                                    )
                                   .FirstOrDefaultAsync(cancellationToken);
        if (userStats == null)
            return RequestResult<GetUserFitnessStatsResponse>.Failure(ResultCode.UserNotFound);
        return RequestResult<GetUserFitnessStatsResponse>.succeeded(userStats, ResultCode.UserFitnessStatsReturnSucess);
    }
}

