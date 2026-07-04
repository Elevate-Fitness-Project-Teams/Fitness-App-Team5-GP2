using Fitness.CalculationEngine.Domain.Entities;
using Fitness.CalculationEngine.Infrastructure.Persistence.Repositories;
using Fitness.CalculationEngine.Shared.Response;
using MediatR;

namespace Fitness.CalculationEngine.Features.UserAssignedPlans.AssignFitnessPlan;

public record AssignFitnessPlanCommand(string UserId, Guid PlanId) : IRequest<RequestResult<bool>>;

public class AssignFitnessPlanCommandHandler (Repository<UserAssignedPlan> repository ,ILogger<AssignFitnessPlanCommandHandler> logger): IRequestHandler<AssignFitnessPlanCommand, RequestResult<bool>>
{
    private readonly ILogger<AssignFitnessPlanCommandHandler> _logger = logger;

    public async Task<RequestResult<bool>> Handle(AssignFitnessPlanCommand request, CancellationToken cancellationToken)
    {
        try
        {
           var userAssignedPlan = new UserAssignedPlan
           {
               UserId = request.UserId,
               PlanId = request.PlanId,
               AssignedAt = DateTime.UtcNow,
               IsActive = true
           };
            repository.Add(userAssignedPlan);
            await repository.SaveChangeAsync(cancellationToken);
            return RequestResult<bool>.succeeded(true, ResultCode.PlanAssignedSuccessfully);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while assigning the fitness plan.");
            return RequestResult<bool>.Failure(ResultCode.PlanAssignmentFailed);
        }
    }
}

