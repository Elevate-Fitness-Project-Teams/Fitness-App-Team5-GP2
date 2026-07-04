using Fitness.CalculationEngine.Infrastructure.Persistence.Repositories;
using Fitness.CalculationEngine.Shared.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Fitness.CalculationEngine.Features.CalculatedMetrics.GetUserCalculatedMetrics;

public record GetUserCalculatedMetricsQuery(string UserId)
    : IRequest<RequestResult<GetUserCalculatedMetricsResponse>>;

public class GetUserCalculatedMetricsQueryHandler(Repository<Domain.Entities.CalculatedMetrics> repository)
    : IRequestHandler<GetUserCalculatedMetricsQuery, RequestResult<GetUserCalculatedMetricsResponse>>
{
    private readonly Repository<Domain.Entities.CalculatedMetrics> _repository = repository;
    public async Task<RequestResult<GetUserCalculatedMetricsResponse>> Handle(GetUserCalculatedMetricsQuery request, CancellationToken cancellationToken)
    {
        var calculatedMetrics = await _repository.Get(s => s.UserId == request.UserId)
            .Select(s => new GetUserCalculatedMetricsResponse(s.Bmr, s.Tdee, s.CalorieTarget, s.Status ,s.CalculatedAt))
            .FirstOrDefaultAsync(cancellationToken);
        if (calculatedMetrics == null)
            return RequestResult<GetUserCalculatedMetricsResponse>.Failure(ResultCode.UserNotFound);
        return RequestResult<GetUserCalculatedMetricsResponse>.succeeded(calculatedMetrics, ResultCode.UserCalculatedMetricsReturnSucess);
    }
}

