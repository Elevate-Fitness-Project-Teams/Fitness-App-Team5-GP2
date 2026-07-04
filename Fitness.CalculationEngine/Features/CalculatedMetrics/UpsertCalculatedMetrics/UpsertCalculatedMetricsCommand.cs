using Fitness.CalculationEngine.Domain.Enums;
using Fitness.CalculationEngine.Domain.Services;
using Fitness.CalculationEngine.Features.CalculatedMetrics.CheckUserCalculatedMatricsExist;
using Fitness.CalculationEngine.Features.UserFitnessStats.GetUserFitnessStats;
using Fitness.CalculationEngine.Infrastructure.Persistence.Repositories;
using Fitness.CalculationEngine.Shared.Response;
using MediatR;

namespace Fitness.CalculationEngine.Features.CalculatedMetrics.Calculate;

public record UpsertCalculatedMetricsCommand
(string UserId) : IRequest<RequestResult<UpsertCalculatedMetricsResponse>>;

public class UpsertCalculatedMetricsCommandHandler(IMediator mediator , CalculationService calculationService 
    ,Repository<Domain.Entities.CalculatedMetrics> repository ,ILogger<UpsertCalculatedMetricsCommandHandler> logger) : IRequestHandler<UpsertCalculatedMetricsCommand, RequestResult<UpsertCalculatedMetricsResponse>>
{
    private readonly IMediator _mediator = mediator;
    private readonly CalculationService _calculationService = calculationService;
    private readonly Repository<Domain.Entities.CalculatedMetrics> _repository = repository;
    private readonly ILogger<UpsertCalculatedMetricsCommandHandler> _logger = logger;

    public async Task<RequestResult<UpsertCalculatedMetricsResponse>> Handle(UpsertCalculatedMetricsCommand request, CancellationToken cancellationToken)
    {
        var userStats = await _mediator.Send(new GetUserFitnessStatsQuery(request.UserId) , cancellationToken);
        if(!userStats.Success)
            return RequestResult<UpsertCalculatedMetricsResponse>.Failure(ResultCode.UserNotFound);

        // Calculate BMR, TDEE, Calorie Target and User Status
        var calculatedMetrics = _calculationService.RunFullCalculationPipeline(request.UserId,
            userStats.Result.Height, userStats.Result.Weight,
            userStats.Result.Age, userStats.Result.Gender, 
            userStats.Result.ActivityLevel, userStats.Result.Goal);

        var existingMetricsResult = await _mediator.Send(new CheckUserCalculatedMatricsExistQuery(request.UserId),cancellationToken);
        
        if (existingMetricsResult.Success)
        {
            calculatedMetrics.Id = existingMetricsResult.Result.Value;
            _repository.SaveInclude(calculatedMetrics,
               nameof(Domain.Entities.CalculatedMetrics.Bmr),
               nameof(Domain.Entities.CalculatedMetrics.Status),
               nameof(Domain.Entities.CalculatedMetrics.Tdee),
               nameof(Domain.Entities.CalculatedMetrics.CalorieTarget));
        }
        else 
          _repository.Add(calculatedMetrics);

        try
        {
            var saveResult = await _repository.SaveChangeAsync(cancellationToken);
            return RequestResult<UpsertCalculatedMetricsResponse>.succeeded(new UpsertCalculatedMetricsResponse
                (calculatedMetrics.Bmr, calculatedMetrics.Tdee, calculatedMetrics.CalorieTarget, Enum.GetName(typeof(UserStatus), calculatedMetrics.Status))
                , ResultCode.CalculatedSucess);
        }
        catch (Exception ex)
        {
            logger.LogError(ex,"Failed to save CalculatedMetrics for UserId {UserId}. Error: {Message}",
                         request.UserId, ex.Message);
            return RequestResult<UpsertCalculatedMetricsResponse>.Failure(ResultCode.CanNotCalculate);
        }
    }
}