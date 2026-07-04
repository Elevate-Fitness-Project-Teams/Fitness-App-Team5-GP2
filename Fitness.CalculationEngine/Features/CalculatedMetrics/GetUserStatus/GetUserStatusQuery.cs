using Fitness.CalculationEngine.Domain.Entities;
using Fitness.CalculationEngine.Domain.Enums;
using Fitness.CalculationEngine.Infrastructure.Persistence.Repositories;
using Fitness.CalculationEngine.Shared.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Fitness.CalculationEngine.Features.CalculatedMetrics.GetUserStatus;

public record GetUserStatusQuery
(string UserId) : IRequest<RequestResult<UserStatusResponse>>;

public record UserStatusResponse(UserStatus Status);

public class GetUserStatusQueryHandler(Repository<Domain.Entities.CalculatedMetrics> repository) : IRequestHandler<GetUserStatusQuery, RequestResult<UserStatusResponse>>
{
    private readonly Repository<Domain.Entities.CalculatedMetrics> _repository = repository;

    public async Task<RequestResult<UserStatusResponse>> Handle(GetUserStatusQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.Get(m => m.UserId == request.UserId)
                               .Select(m => m.Status)
                               .FirstOrDefaultAsync(cancellationToken);
        if(result == 0)
            return RequestResult<UserStatusResponse>.Failure(ResultCode.ThereIsNoCalculatedMetricsForThisUser);
        return RequestResult<UserStatusResponse>.succeeded(new UserStatusResponse(result),ResultCode.UserStatusRetrived);
    }
}