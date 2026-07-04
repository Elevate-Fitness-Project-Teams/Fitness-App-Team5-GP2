using BuildingBlocks.Models;
using Fitness.CalculationEngine.Features.FitnessPlanConfigurations.Common;
using Fitness.CalculationEngine.Shared.Constants;
using Fitness.CalculationEngine.Shared.Interfaces;
using Fitness.CalculationEngine.Shared.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.CalculationEngine.Features.FitnessPlanConfigurations.GetFitnessPlanConfigurations;

public class GetFitnessPlanConfigurationsEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet($"{Endpoints.FitnessPlanConfig.GetFitnessPlanConfig}",
            async ([AsParameters] GetFitnessPlanConfigurationsQuery request, IMediator mediator) =>
            {
                var result = await mediator.Send(request);
                return Results.Ok(ApiResponse<PaginatedResult<GetFitnessPlanConfigurationResponse>>.Successed(result.Result));
            });
    }
}
