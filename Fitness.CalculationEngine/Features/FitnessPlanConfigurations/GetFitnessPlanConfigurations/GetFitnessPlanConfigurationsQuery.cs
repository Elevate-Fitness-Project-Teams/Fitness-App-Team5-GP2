using Fitness.CalculationEngine.Domain.Entities;
using Fitness.CalculationEngine.Domain.Enums;
using Fitness.CalculationEngine.Domain.Services;
using Fitness.CalculationEngine.Features.FitnessPlanConfigurations.Common;
using Fitness.CalculationEngine.Infrastructure.Persistence.Repositories;
using Fitness.CalculationEngine.Shared.Requests;
using Fitness.CalculationEngine.Shared.Response;
using Fitness.CalculationEngine.Shared.Services;
using MediatR;

namespace Fitness.CalculationEngine.Features.FitnessPlanConfigurations.GetFitnessPlanConfigurations;

public record GetFitnessPlanConfigurationsQuery
(string? Goal, string? Status,int? PageNumber, int? PageSize) : IRequest<RequestResult<PaginatedResult<GetFitnessPlanConfigurationResponse>>>;

public class GetFitnessPlanConfigurationsQueryHandler(Repository<FitnessPlanConfig> repository)
    : IRequestHandler<GetFitnessPlanConfigurationsQuery, RequestResult<PaginatedResult<GetFitnessPlanConfigurationResponse>>>
{
    private readonly Repository<FitnessPlanConfig> _repository = repository;

    public async Task<RequestResult<PaginatedResult<GetFitnessPlanConfigurationResponse>>> Handle(GetFitnessPlanConfigurationsQuery request, CancellationToken cancellationToken)
    {
        var query = _repository.Get();

        if(request.Goal is not null && FitnessGoalParser.TryParse(request.Goal, out var goal))
            query = query.Where(p => p.Goal == goal);

        if (request.Status is not null && Enum.TryParse(typeof(UserStatus),request.Status,true,  out var status))
            query = query.Where(p => p.Status == (UserStatus)status);
        var selectQuery = query.Select(p =>
                   new GetFitnessPlanConfigurationResponse(
                       p.Id,
                       p.Name,
                       p.Description, 
                       p.MinCalorie,
                       p.MaxCalorie,
                       p.WorkoutsPerWeek, 
                       p.EstimatedDuration,
                       p.ProgramType, 
                       p.Goal.ToString(),
                       p.Status.ToString()));
        var pagedResult = await selectQuery.PaginateAsync(new PaginationParams
        {
            Page = request.PageNumber ?? 0 ,
            PerPage = request.PageSize ?? 0 ,
        },cancellationToken);

        return RequestResult<PaginatedResult<GetFitnessPlanConfigurationResponse>>.succeeded(pagedResult,ResultCode.PlansRetrivedSuccessfully);
    }
}      