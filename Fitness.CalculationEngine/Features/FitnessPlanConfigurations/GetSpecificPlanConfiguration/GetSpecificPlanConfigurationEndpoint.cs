using BuildingBlocks.Models;
using Fitness.CalculationEngine.Features.FitnessPlanConfigurations.Common;
using Fitness.CalculationEngine.Shared.Constants;
using Fitness.CalculationEngine.Shared.Interfaces;
using Fitness.CalculationEngine.Shared.Response;
using MediatR;

namespace Fitness.CalculationEngine.Features.FitnessPlanConfigurations.GetSpecificPlanConfiguration;

public class GetSpecificPlanConfigurationEndpoint :IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet($"{Endpoints.FitnessPlanConfig.GetSpecificPlanConfig}",
            async (Guid planId, IMediator mediator) =>
            {
                var result = await mediator.Send(new GetSpecificPlanConfigurationQuery(planId));
                return result.Code switch
                {
                    ResultCode.PlanConfigurationNotFound => Results.NotFound(ApiResponse<GetFitnessPlanConfigurationResponse>.Failure("FCE_PLAN_CONFIG_NOT_FOUND.")),
                    ResultCode.PlanConfigurationRetrieved => Results.Ok(ApiResponse<GetFitnessPlanConfigurationResponse>.Successed(result.Result)),
                };
            });
    }
}
