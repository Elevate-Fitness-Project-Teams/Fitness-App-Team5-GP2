using Fitness.CalculationEngine.Domain.Enums;
using Fitness.CalculationEngine.Domain.Services;
using Fitness.CalculationEngine.Features.UserFitnessStats.CheckUserFitnessStateIsExist;
using Fitness.CalculationEngine.Features.UserFitnessStats.GetUserFitnessStats;
using Fitness.CalculationEngine.Infrastructure.Persistence.Repositories;
using Fitness.CalculationEngine.Shared.Response;
using MediatR;

namespace Fitness.CalculationEngine.Features.CalculatedMetrics.Calculate;

public record CalculateCommand
(string UserId) : IRequest<RequestResult<CalculateResponse>>;

public class CalculateCommandHandler(IMediator mediator , CalculationService calculationService 
    ,Repository<Domain.Entities.CalculatedMetrics> repository) : IRequestHandler<CalculateCommand, RequestResult<CalculateResponse>>
{
    private readonly IMediator _mediator = mediator;
    private readonly CalculationService _calculationService = calculationService;
    private readonly Repository<Domain.Entities.CalculatedMetrics> _repository = repository;

    public async Task<RequestResult<CalculateResponse>> Handle(CalculateCommand request, CancellationToken cancellationToken)
    {
        var isUserExist = await _mediator.Send(new CheckUserFitnessStateIsExistQuery(request.UserId));
        if (!isUserExist.Result)
            return RequestResult<CalculateResponse>.Failure(ResultCode.UserNotFound);
        var userStats = await _mediator.Send(new GetUserFitnessStatsQuery(request.UserId) , cancellationToken);
        if(!userStats.Success)
            return RequestResult<CalculateResponse>.Failure(ResultCode.UserNotFound);
        var bmr = _calculationService.CalculateBMR(userStats.Result.Gender, userStats.Result.Weight, userStats.Result.Height, userStats.Result.Age);
        var tdee = _calculationService.CalculateTDEE(bmr, userStats.Result.ActivityLevel);
        var calorieTarget = _calculationService.CalculateCalorieTarget(tdee, userStats.Result.Goal);
        var userStatus = _calculationService.GetUserStatus(calorieTarget);
        var calculatedMetrics = new Domain.Entities.CalculatedMetrics
        {
            Bmr = bmr,
            Status = userStatus,
            Tdee = tdee,
            UserId = request.UserId,
            CalorieTarget = calorieTarget,
        };
        _repository.Add(calculatedMetrics);
        try
        {
            var noOfEffectedRows = await _repository.SaveChangeAsync(cancellationToken);
            if(noOfEffectedRows == 0)
                return RequestResult<CalculateResponse>.Failure(ResultCode.CanNotCalculate);
            return RequestResult<CalculateResponse>.succeeded(new CalculateResponse(bmr,tdee,calorieTarget,Enum.GetName(typeof(UserStatus),userStatus)),ResultCode.CalculatedSucess);
        }
        catch (Exception ex)
        {
            return RequestResult<CalculateResponse>.Failure(ResultCode.CanNotCalculate);
        }
    }
}