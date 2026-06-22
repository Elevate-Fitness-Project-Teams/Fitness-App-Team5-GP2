using System.Net;

namespace BuildingBlocks.Models;

public class ApiResponse<T>
{

    public T Data { get; set; }
    public string Message { get; set; }
    public bool Success { get; set; }
    public HttpStatusCode StatusCode { get; set; }
    public DateTime Timestamp { get; set; }
    public List<string> Errors { get; set; }
    public ApiResponse() { }
    public ApiResponse(bool success, T? data, List<string> errors, HttpStatusCode statusCode = HttpStatusCode.OK , string message ="")
    {
        Success = success;
        Message = message;
        Data = data;
        Errors = errors ?? new List<string>();
        StatusCode = statusCode;
        Timestamp = DateTime.Now;
    }
    public static ApiResponse<T> Successed(T? data, HttpStatusCode statusCode = HttpStatusCode.OK)
        => new ApiResponse<T>(
            success: true,
            data: data,
            errors: new List<string>(),
            statusCode: statusCode);

    public static ApiResponse<T> Failure(
        string errorMessage,
        HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        => new ApiResponse<T>(false, default, new List<string> { errorMessage }, statusCode);

    public static ApiResponse<T> Failure(List<string> errors, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        => new ApiResponse<T>(false, default, errors ?? new List<string>(), statusCode);
}

