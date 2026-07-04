using Fitness.CalculationEngine.Infrastructure.Persistence.Repositories;
using Fitness.CalculationEngine.Shared.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Fitness.CalculationEngine.Features.CalculatedMetrics.CheckUserCalculatedMatricsExist;

public record CheckUserCalculatedMatricsExistQuery
(string UserId) : IRequest<RequestResult<Guid?>>;

public class CheckUserCalculatedMatricsExistQueryHandler(Repository<Domain.Entities.CalculatedMetrics> repository)
    : IRequestHandler<CheckUserCalculatedMatricsExistQuery, RequestResult<Guid?>>
{
    private readonly Repository<Domain.Entities.CalculatedMetrics> _repository = repository;
    public async Task<RequestResult<Guid?>> Handle(CheckUserCalculatedMatricsExistQuery request, CancellationToken cancellationToken)
    {
        var calculatedMetricsId = await _repository.Get(cm => cm.UserId == request.UserId)
                                      .Select(cm => cm.Id)
                                      .FirstOrDefaultAsync(cancellationToken);
        if(calculatedMetricsId == Guid.Empty)
            return RequestResult<Guid?>.Failure(ResultCode.NotFoundUserCalculatedMetrics);
        return RequestResult<Guid?>.succeeded(calculatedMetricsId, ResultCode.UserCalculatedMetricsExistBefore);
    }
}
