using Fitness.CalculationEngine.Domain.Entities;
using Fitness.CalculationEngine.Domain.Enums;
using Fitness.CalculationEngine.Features.UserFitnessStats.CheckUserFitnessStateIsExist;
using Fitness.CalculationEngine.Infrastructure.Persistence.Repositories;
using Fitness.CalculationEngine.Shared.Response;
using Mapster;
using MediatR;

namespace Fitness.CalculationEngine.Features.UserFitnessStats.WeightGoalActivity;

public record WeightGoalActivityCommand(string UserId , double Weight , double Height ,int Age ,string Gender
    , string Goal , string ActivityLevel) : IRequest<RequestResult<WeightGoalActivityResponse>>;

public class WeightGoalActivityCommandHandler(Repository<UserFitnessStat> repository ,IMediator mediator) 
    : IRequestHandler<WeightGoalActivityCommand, RequestResult<WeightGoalActivityResponse>>
{
    private readonly Repository<UserFitnessStat> _repository = repository;
    private readonly IMediator _mediator = mediator;

    public async Task<RequestResult<WeightGoalActivityResponse>> Handle(WeightGoalActivityCommand request, CancellationToken cancellationToken)
    {
        var isUserStatsExistBefore = await _mediator.Send(new CheckUserFitnessStateIsExistQuery(request.UserId));
        if (!isUserStatsExistBefore.Success)
            return RequestResult<WeightGoalActivityResponse>.Failure(null,isUserStatsExistBefore.Code);

        Enum.TryParse<ActivityLevel>(request.ActivityLevel, true, out var activityLevel);
        Enum.TryParse<Goal>(request.Goal, true, out var goal);
        Enum.TryParse<Gender>(request.Gender, true, out var gender);
        var userState = request.Adapt<UserFitnessStat>();
        return null;
    }
}

