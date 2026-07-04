using Fitness.CalculationEngine.Domain.Entities;
using Fitness.CalculationEngine.Infrastructure.Persistence.Repositories;
using Fitness.CalculationEngine.Shared.Response;
using MediatR;

namespace Fitness.CalculationEngine.Features.UserAssignedPlans.SetAllOldUserFitnessPlansNotActive;

public record SetAllOldUserFitnessPlansNotActiveCommand
(string UserId) : IRequest<RequestResult<bool>>;

public class SetAllOldUserFitnessPlansNotActiveCommandHandler(Repository<UserAssignedPlan> repository) : IRequestHandler<SetAllOldUserFitnessPlansNotActiveCommand, RequestResult<bool>>
{
    private readonly Repository<UserAssignedPlan> _repository = repository;
    public async Task<RequestResult<bool>> Handle(SetAllOldUserFitnessPlansNotActiveCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var updateResult = await _repository.BulkUpdateAsync(
                                   predicate: x => x.UserId == request.UserId && x.IsActive,
                                   updateProp: x => x.IsActive,
                                   newValue: false,
                                   cancellationToken: cancellationToken
           );
            return RequestResult<bool>.succeeded(true, ResultCode.OldPlansUpdatedSuccessfully);
        }
        catch (Exception ex)
        {
            return RequestResult<bool>.Failure(true, ResultCode.CanNotUpdateOldPlans);
        }
    }
}