namespace Company.ProjectName.Shared.Response;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public int StatusCode { get; set; }
    public string Message { get; set; } = string.Empty;
    public List<string> Errors { get; set; } = [];
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public T? Result { get; set; }

    public static ApiResponse<T> Ok(T result, string message = "Success") =>
        new() { Success = true, StatusCode = 200, Message = message, Result = result };

    public static ApiResponse<T> Created(T result, string message = "Created") =>
        new() { Success = true, StatusCode = 201, Message = message, Result = result };

    public static ApiResponse<T> Error(int statusCode, string message, List<string>? errors = null) =>
        new() { Success = false, StatusCode = statusCode, Message = message, Errors = errors ?? [] };
}
