using Fitness.CalculationEngine.Shared.Enums;

namespace Fitness.CalculationEngine.Shared.Models;

public record RequestResult<T>(T Data, bool IsSuccess, string Message, ErrorCode ErrorCode)
{
    public static RequestResult<T> Success(T data, SuccessCode SuccessCode)
    {
        return new RequestResult<T>(data, true, SuccessCode.ToString(), ErrorCode.None);
    }

    public static RequestResult<T> Success(T data)
    {
        return new RequestResult<T>(data, true, string.Empty, ErrorCode.None);
    }
    public static RequestResult<T> Success()
    {
        return new RequestResult<T>(default, true, string.Empty, ErrorCode.None);
    }

    public static RequestResult<T> Failure(ErrorCode errorCode)
    {
        string message = errorCode.ToString();

        return new RequestResult<T>(default, false, message, errorCode);
    }

    public static RequestResult<T> Failure(ErrorCode errorCode, string message)
    {
        return new RequestResult<T>(default, false, message, errorCode);
    }

    public static RequestResult<T> Failure()
    {
        return new RequestResult<T>(default, false, null, ErrorCode.None);
    }
}