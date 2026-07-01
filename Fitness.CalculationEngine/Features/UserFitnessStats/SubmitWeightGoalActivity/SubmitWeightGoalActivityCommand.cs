using Fitness.CalculationEngine.Domain.Entities;
using Fitness.CalculationEngine.Domain.Enums;
using Fitness.CalculationEngine.Domain.Services;
using Fitness.CalculationEngine.Features.UserFitnessStats.CheckUserFitnessStateIsExist;
using Fitness.CalculationEngine.Infrastructure.Persistence.Repositories;
using Fitness.CalculationEngine.Shared.Response;
using MediatR;

namespace Fitness.CalculationEngine.Features.UserFitnessStats.WeightGoalActivity;

public record SubmitWeightGoalActivityCommand(string UserId , double Weight , double Height ,int Age ,string Gender
    , string Goal , string ActivityLevel) : IRequest<RequestResult<SubmitWeightGoalActivityResponse>>;

public class WeightGoalActivityCommandHandler(Repository<UserFitnessStat> repository ,IMediator mediator) 
    : IRequestHandler<SubmitWeightGoalActivityCommand, RequestResult<SubmitWeightGoalActivityResponse>>
{
    private readonly Repository<UserFitnessStat> _repository = repository;
    private readonly IMediator _mediator = mediator;

    public async Task<RequestResult<SubmitWeightGoalActivityResponse>> Handle(SubmitWeightGoalActivityCommand request, CancellationToken cancellationToken)
    {
        var isUserStatsExistBefore = await _mediator.Send(new CheckUserFitnessStateIsExistQuery(request.UserId));
        if (isUserStatsExistBefore.Result)
            return RequestResult<SubmitWeightGoalActivityResponse>.Failure(isUserStatsExistBefore.Code);
        if (!Enum.TryParse<Gender>(request.Gender, ignoreCase: true, out var gender))
            return RequestResult<SubmitWeightGoalActivityResponse>.Failure(ResultCode.InvalidGender);

        if (!FitnessGoalParser.TryParse(request.Goal, out var goal))
            return RequestResult<SubmitWeightGoalActivityResponse>.Failure(ResultCode.InvalidGoal);

        if (!Enum.TryParse<ActivityLevel>(request.ActivityLevel, ignoreCase: true, out var activityLevel))
            return RequestResult<SubmitWeightGoalActivityResponse>.Failure(ResultCode.InvalidActivityLevel);

        var userStat = new UserFitnessStat
        {
           UserId = request.UserId,
           Weight = request.Weight,
           Height = request.Height,
           Age = request.Age,
           Gender = gender,
           Goal = goal,
           ActivityLevel = activityLevel
        };

        _repository.Add(userStat);
        try
        {
            var result = await _repository.SaveChangeAsync(cancellationToken);
            return RequestResult<SubmitWeightGoalActivityResponse>.succeeded(new SubmitWeightGoalActivityResponse
                 (userStat.Id, userStat.UserId, userStat.RecordedAt), ResultCode.StatsCreatedSuccesses);
        }
        catch (Exception ex)
        {
            return RequestResult<SubmitWeightGoalActivityResponse>.Failure(ResultCode.CanNotCreateUserFitnessStats);
        }
    }
}

