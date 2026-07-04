using Fitness.CalculationEngine.Domain.Entities;
using Fitness.CalculationEngine.Features.FitnessPlanConfigurations.Common;
using Fitness.CalculationEngine.Infrastructure.Persistence.Repositories;
using Fitness.CalculationEngine.Shared.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Fitness.CalculationEngine.Features.FitnessPlanConfigurations.GetSpecificPlanConfiguration;

public record GetSpecificPlanConfigurationQuery
(Guid PlanId) : IRequest<RequestResult<GetFitnessPlanConfigurationResponse>>;

public class GetSpecificPlanConfigurationQueryHandler (Repository<FitnessPlanConfig> repository):
    IRequestHandler<GetSpecificPlanConfigurationQuery, RequestResult<GetFitnessPlanConfigurationResponse>>
{
    private readonly Repository<FitnessPlanConfig> _repository = repository;

    public async Task<RequestResult<GetFitnessPlanConfigurationResponse>> Handle(GetSpecificPlanConfigurationQuery request, CancellationToken cancellationToken)
    {
        var planConfiguration = await _repository.Get(p => p.Id == request.PlanId)
                                                 .Select(p => new GetFitnessPlanConfigurationResponse
                                                     (p.Id, p.Name, p.Description, p.MinCalorie, p.MaxCalorie, p.WorkoutsPerWeek, p.EstimatedDuration, p.ProgramType, p.Goal.ToString(), p.Status.ToString())
                                                  )
                                                 .FirstOrDefaultAsync(cancellationToken);
        if (planConfiguration == null)
            return RequestResult<GetFitnessPlanConfigurationResponse>.Failure(ResultCode.PlanConfigurationNotFound);

        return RequestResult<GetFitnessPlanConfigurationResponse>.succeeded(planConfiguration, ResultCode.PlanConfigurationRetrieved);
    }
}