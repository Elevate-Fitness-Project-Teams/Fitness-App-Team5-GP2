using Fitness.CalculationEngine.Features.CalculatedMetrics.Calculate;
using Fitness.CalculationEngine.Features.CalculatedMetrics.GetUserCalculatedMetrics;
using Fitness.Shared.Protos;
using Grpc.Core;
using MediatR;
using Microsoft.Identity.Client;

namespace Fitness.CalculationEngine.Infrastructure.Integration.GRPC;

public class FceGrpcService : FitnessCalculationService.FitnessCalculationServiceBase
{
    private readonly IMediator _mediator;  // Your existing service
    private readonly ILogger<FceGrpcService> _logger;

    public FceGrpcService(
        IMediator mediator,
        ILogger<FceGrpcService> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
    public override async Task<GetMetricsResponse> GetUserMetrics(
        GetMetricsRequest request,
        ServerCallContext context)
    {
        try
        {
            _logger.LogInformation("gRPC GetUserMetrics called for UserId: {UserId}", request.UserId);

            // ⭐ Call your EXISTING business logic
            var metrics = await _mediator.Send(new GetUserCalculatedMetricsQuery(request.UserId));

            if (!metrics.Success)
            {
                return new GetMetricsResponse
                {
                    Success = false,
                    ErrorCode = "FCE_METRICS_NOT_FOUND",
                    ErrorMessage = $"No metrics found for user {request.UserId}"
                };
            }

            // ⭐ Map your domain model to gRPC response
            return new GetMetricsResponse
            {
                Success = true,
                Data = new UserMetrics
                {
                    UserId = request.UserId,
                    Bmr = metrics.Result.Bmr,
                    Tdee = metrics.Result.Tdee,
                    CalorieTarget = metrics.Result.CalorieTarget,
                    Status = metrics.Result.Status.ToString(),
      
                    CalculatedAt = metrics.Result.CalculatedAt.ToString("o")
                }
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetUserMetrics for UserId: {UserId}", request.UserId);
            return new GetMetricsResponse
            {
                Success = false,
                ErrorCode = "FCE_INTERNAL_ERROR",
                ErrorMessage = "An internal error occurred while retrieving metrics"
            };
        }
    }

    /// <summary>
    /// Calculate metrics for a user (triggers recalculation)
    /// Called by: Nutrition Service (when needed)
    /// </summary>
    public override async Task<CalculateMetricsResponse> CalculateMetrics(
        CalculateMetricsRequest request,
        ServerCallContext context)
    {
        try
        {
            _logger.LogInformation("gRPC CalculateMetrics called for UserId: {UserId}", request.UserId);

            // ⭐ Call your EXISTING business logic
            var metrics = await _mediator.Send(new UpsertCalculatedMetricsCommand(request.UserId));

            if (!metrics.Success)
            {
                return new CalculateMetricsResponse
                {
                    Success = false,
                    ErrorCode = "FCE_CALCULATION_FAILED",
                    ErrorMessage = $"Failed to calculate metrics for user {request.UserId}"
                };
            }

            return new CalculateMetricsResponse
            {
                Success = true,
                Data = new UserMetrics
                {
                    UserId = request.UserId,
                    Bmr = metrics.Result.Bmr,
                    Tdee = metrics.Result.Tdee,
                    CalorieTarget = metrics.Result.CalorieTarget,
                    Status = metrics.Result.UserStatus,
                }
            };
        }
        //catch (FceValidationException ex)
        //{
        //    return new CalculateMetricsResponse
        //    {
        //        Success = false,
        //        ErrorCode = "FCE_VALIDATION_ERROR",
        //        ErrorMessage = ex.Message
        //    };
        //}
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in CalculateMetrics for UserId: {UserId}", request.UserId);
            return new CalculateMetricsResponse
            {
                Success = false,
                ErrorCode = "FCE_INTERNAL_ERROR",
                ErrorMessage = "An internal error occurred during calculation"
            };
        }
    }

    /// <summary>
    /// Check if user has metrics calculated
    /// Called by: Nutrition Service (quick check before calling GetUserMetrics)
    /// </summary>
    public override async Task<HasMetricsResponse> HasMetrics(
        HasMetricsRequest request,
        ServerCallContext context)
    {
        try
        {
            _logger.LogInformation("gRPC HasMetrics called for UserId: {UserId}", request.UserId);

            //// ⭐ Call your EXISTING business logic
            //var hasMetrics = await _fceService.HasMetricsAsync(request.UserId);

            return new HasMetricsResponse
            {
                HasMetrics = /*hasMetrics*/ true
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in HasMetrics for UserId: {UserId}", request.UserId);
            return new HasMetricsResponse
            {
                HasMetrics = false
            };
        }
    }
}