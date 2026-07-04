using Fitness.CalculationEngine.Domain.Entities;
using Fitness.CalculationEngine.Domain.Enums;
using Fitness.CalculationEngine.Infrastructure.Persistence.Repositories;
using Fitness.CalculationEngine.Shared.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Fitness.CalculationEngine.Features.UserAssignedPlans.GetGoalAndStatusMatchedPlan;

public record GetGoalAndStatusMatchedPlanQuery
(FitnessGoal Goal, UserStatus UserStatus): IRequest<RequestResult<GetGoalAndStatusMatchedPlanResponse>>;

public record GetGoalAndStatusMatchedPlanResponse(Guid Id,string Name, string Description, double MinCalorie
    , double MaxCalorie, int WorkoutsPerWeek, string EstimatedDuration, string ProgramType);

public class GetGoalAndStatusMatchedPlanQueryHandler(Repository<FitnessPlanConfig> repository) : IRequestHandler<GetGoalAndStatusMatchedPlanQuery, RequestResult<GetGoalAndStatusMatchedPlanResponse>>
{
    private readonly Repository<FitnessPlanConfig> _repository = repository;

    public async Task<RequestResult<GetGoalAndStatusMatchedPlanResponse>> Handle(GetGoalAndStatusMatchedPlanQuery request, CancellationToken cancellationToken)
    {
        var matchedPlan = await _repository.Get(x => x.Goal == request.Goal
                                       && x.Status == request.UserStatus)
                                       .Select(p => new GetGoalAndStatusMatchedPlanResponse(
                                           p.Id,
                                           p.Name,
                                           p.Description,
                                           p.MinCalorie,
                                           p.MaxCalorie,
                                           p.WorkoutsPerWeek,
                                           p.EstimatedDuration,
                                           p.ProgramType
                                       ))
                                       .FirstOrDefaultAsync(cancellationToken);
        if (matchedPlan is null)
            return RequestResult<GetGoalAndStatusMatchedPlanResponse>.Failure(ResultCode.NotMatchedPlanFound);

        return RequestResult<GetGoalAndStatusMatchedPlanResponse>.succeeded(matchedPlan ,ResultCode.PlanRetrivedSuccessfully);
    }
}