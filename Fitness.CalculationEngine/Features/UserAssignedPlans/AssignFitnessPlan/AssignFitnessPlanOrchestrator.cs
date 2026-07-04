using Fitness.CalculationEngine.Features.CalculatedMetrics.GetUserStatus;
using Fitness.CalculationEngine.Features.UserAssignedPlans.GetGoalAndStatusMatchedPlan;
using Fitness.CalculationEngine.Features.UserAssignedPlans.SetAllOldUserFitnessPlansNotActive;
using Fitness.CalculationEngine.Features.UserFitnessStats.GetUserFitnessGoal;
using Fitness.CalculationEngine.Shared.Response;
using MediatR;

namespace Fitness.CalculationEngine.Features.UserAssignedPlans.AssignFitnessPlan;

public record AssignFitnessPlanOrchestrator
(string UserId):IRequest<RequestResult<AssignFitnessPlanResponse>>;

public record AssignFitnessPlanResponse(string Name, string Description,double MinCalorie
    , double MaxCalorie ,int WorkoutsPerWeek ,string EstimatedDuration ,string ProgramType);

public class AssignFitnessPlanOrchestratorHandler(IMediator mediator, ILogger<AssignFitnessPlanOrchestratorHandler> logger) : IRequestHandler<AssignFitnessPlanOrchestrator, RequestResult<AssignFitnessPlanResponse>>
{
    private readonly IMediator _mediator = mediator;
    private readonly ILogger<AssignFitnessPlanOrchestratorHandler> _logger = logger;
    public async Task<RequestResult<AssignFitnessPlanResponse>> Handle(AssignFitnessPlanOrchestrator request, CancellationToken cancellationToken)
    {
        var userGoal = await _mediator.Send(new GetUserFitnessGoalQuery(request.UserId), cancellationToken);
        if(!userGoal.Success)
            return RequestResult<AssignFitnessPlanResponse>.Failure(userGoal.Code);

        var userStatus = await _mediator.Send(new GetUserStatusQuery(request.UserId), cancellationToken);
        if(!userStatus.Success)
            return RequestResult<AssignFitnessPlanResponse>.Failure(userStatus.Code);
      
        try
        {
            var fitnessPlan = await _mediator.Send(new GetGoalAndStatusMatchedPlanQuery(userGoal.Result, userStatus.Result.Status), cancellationToken);
            if (!fitnessPlan.Success)
                return RequestResult<AssignFitnessPlanResponse>.Failure(fitnessPlan.Code);

            var setAllOldPlansDeActive = await _mediator.Send(new SetAllOldUserFitnessPlansNotActiveCommand(request.UserId), cancellationToken);
            if (!setAllOldPlansDeActive.Success)
                return RequestResult<AssignFitnessPlanResponse>.Failure(setAllOldPlansDeActive.Code);
            var assignResult = await _mediator.Send(new AssignFitnessPlanCommand(request.UserId, fitnessPlan.Result.Id), cancellationToken);
            if(!assignResult.Success)
                return RequestResult<AssignFitnessPlanResponse>.Failure(assignResult.Code);
            return RequestResult<AssignFitnessPlanResponse>.succeeded(new AssignFitnessPlanResponse(fitnessPlan.Result.Name, fitnessPlan.Result.Description, fitnessPlan.Result.MinCalorie, fitnessPlan.Result.MaxCalorie, fitnessPlan.Result.WorkoutsPerWeek, fitnessPlan.Result.EstimatedDuration, fitnessPlan.Result.ProgramType), ResultCode.PlanAssignedSuccessfully);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while orchestrating the fitness plan assignment.");
            return RequestResult<AssignFitnessPlanResponse>.Failure(ResultCode.PlanAssignmentFailed);
        }

    }
}