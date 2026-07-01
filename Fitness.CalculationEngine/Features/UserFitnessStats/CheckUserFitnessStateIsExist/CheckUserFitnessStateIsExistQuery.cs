using Fitness.CalculationEngine.Domain.Entities;
using Fitness.CalculationEngine.Infrastructure.Persistence.Repositories;
using Fitness.CalculationEngine.Shared.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Fitness.CalculationEngine.Features.UserFitnessStats.CheckUserFitnessStateIsExist;

public record CheckUserFitnessStateIsExistQuery
    (string UserId) : IRequest<RequestResult<bool>>;

public class CheckUserFitnessStateIsExistQueryHandler(Repository<Domain.Entities.UserFitnessStat> repository) 
    : IRequestHandler<CheckUserFitnessStateIsExistQuery, RequestResult<bool>>
{
    private readonly Repository<Domain.Entities.UserFitnessStat> _repository = repository;

    public async Task<RequestResult<bool>> Handle(CheckUserFitnessStateIsExistQuery request, CancellationToken cancellationToken)
    {
        var isExist = await _repository
                     .Get()
                     .AnyAsync(s => s.UserId == request.UserId);
        return RequestResult<bool>.succeeded(isExist ,ResultCode.UserIsAlreadyExist);
    }
}

