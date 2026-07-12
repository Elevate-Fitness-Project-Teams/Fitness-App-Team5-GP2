using Fitness.Shared.Models;
using Fitness.Shared.Protos;
using Grpc.Core;
using Grpc.Net.Client;

namespace Fitness.Nutrition.Infrastructure.Integrations.FCEService;

public class FceGrpcClient : IDisposable
{
    private readonly GrpcChannel _channel;
    private readonly FitnessCalculationService.FitnessCalculationServiceClient _client;
    private readonly ILogger<FceGrpcClient> _logger;
    private readonly IConfiguration _configuration;
    private bool _disposed;

    public FceGrpcClient(
        IConfiguration configuration,
        ILogger<FceGrpcClient> logger)
    {
        _configuration = configuration;
        _logger = logger;

        var fceUrl = configuration["Services:FCE:GrpcUrl"]
                 ?? throw new InvalidOperationException("Services:FCE:GrpcUrl is not configured");

        // Only use insecure handler for HTTPS localhost development
        GrpcChannelOptions options = new GrpcChannelOptions
        {
            MaxRetryAttempts = 3,
            MaxRetryBufferSize = 1024 * 1024
        };

        if (fceUrl.StartsWith("https://localhost"))
        {
            var httpHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
                   HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };
            options.HttpHandler = httpHandler;
        }

        _channel = GrpcChannel.ForAddress(fceUrl, options);
        _client = new FitnessCalculationService.FitnessCalculationServiceClient(_channel);
    }

    public async Task<ServiceResult<FitnessMetrics>> GetUserMetricsAsync(
        string userId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Calling FCE gRPC: GetUserMetrics for UserId {UserId}", userId);

            var request = new GetMetricsRequest { UserId = userId };
            var response = await _client.GetUserMetricsAsync(
                request,
                cancellationToken: cancellationToken,
                deadline: DateTime.UtcNow.AddSeconds(15));

            if (!response.Success)
            {
                _logger.LogWarning("FCE returned error: {ErrorCode} - {ErrorMessage}",
                    response.ErrorCode, response.ErrorMessage);

                return new ServiceResult<FitnessMetrics>
                {
                    IsSuccess = false,
                    ErrorCode = response.ErrorCode,
                    ErrorMessage = response.ErrorMessage
                };
            }

            var metrics = MapToFitnessMetrics(response.Data);

            return new ServiceResult<FitnessMetrics>
            {
                IsSuccess = true,
                Data = metrics
            };
        }
        catch (RpcException ex) when (ex.StatusCode == StatusCode.Unavailable)
        {
            _logger.LogError(ex, "FCE gRPC service unavailable");
            return new ServiceResult<FitnessMetrics>
            {
                IsSuccess = false,
                ErrorCode = "SRV_SERVICE_UNAVAILABLE",
                ErrorMessage = "FCE service is currently unavailable"
            };
        }
        catch (RpcException ex) when (ex.StatusCode == StatusCode.DeadlineExceeded)
        {
            _logger.LogError(ex, "FCE gRPC request timed out");
            return new ServiceResult<FitnessMetrics>
            {
                IsSuccess = false,
                ErrorCode = "SRV_REQUEST_TIMEOUT",
                ErrorMessage = "Request to FCE service timed out"
            };
        }
        catch (RpcException ex)
        {
            _logger.LogError(ex, "gRPC error calling FCE: {StatusCode} - {Message}",
                ex.StatusCode, ex.Message);

            return new ServiceResult<FitnessMetrics>
            {
                IsSuccess = false,
                ErrorCode = "SRV_GRPC_ERROR",
                ErrorMessage = $"gRPC error: {ex.StatusCode} - {ex.Message}"
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error calling FCE gRPC");
            return new ServiceResult<FitnessMetrics>
            {
                IsSuccess = false,
                ErrorCode = "SRV_UNKNOWN_ERROR",
                ErrorMessage = "An unexpected error occurred"
            };
        }
    }

    public async Task<ServiceResult<FitnessMetrics>> CalculateMetricsAsync(
        string userId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var request = new CalculateMetricsRequest { UserId = userId };
            var response = await _client.CalculateMetricsAsync(
                request,
                cancellationToken: cancellationToken,
                deadline: DateTime.UtcNow.AddSeconds(30));

            if (!response.Success)
            {
                return new ServiceResult<FitnessMetrics>
                {
                    IsSuccess = false,
                    ErrorCode = response.ErrorCode,
                    ErrorMessage = response.ErrorMessage
                };
            }

            var metrics = MapToFitnessMetrics(response.Data);

            return new ServiceResult<FitnessMetrics>
            {
                IsSuccess = true,
                Data = metrics
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error calling FCE CalculateMetrics");
            return new ServiceResult<FitnessMetrics>
            {
                IsSuccess = false,
                ErrorCode = "SRV_CALCULATE_ERROR",
                ErrorMessage = ex.Message
            };
        }
    }
    private FitnessMetrics MapToFitnessMetrics(UserMetrics protoMetrics)
    {
        return new FitnessMetrics
        {
            UserId = protoMetrics.UserId,
            Bmr = protoMetrics.Bmr,
            Tdee = protoMetrics.Tdee,
            CalorieTarget = protoMetrics.CalorieTarget,
            Status = protoMetrics.Status,
            Goal = protoMetrics.Goal,
            ActivityLevel = protoMetrics.ActivityLevel,
            Weight = protoMetrics.Weight,
            Height = protoMetrics.Height,
            Age = protoMetrics.Age,
            Gender = protoMetrics.Gender,
            CalculatedAt = DateTime.Parse(protoMetrics.CalculatedAt)
        };
    }

    public void Dispose()
    {
        if (!_disposed)
        {
            _channel?.Dispose();
            _disposed = true;
        }
    }
}
