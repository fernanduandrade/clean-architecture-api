namespace Bot.Application.Common;

public class ApiResult<T> : IDisposable
{
    public string Message { get; set; }
    public T? Data { get; set; }
    public string[]? Errors { get; set; }

    public ApiResult(T? data, string message = "", string[]? errors = null)
    {
        Data = data;
        Message = message;
        Errors = errors;
    }

    public ApiResult(string message, string[] errors)
    {
        Message = message;
        Errors = errors;
    }

    public void Dispose()
    {
        if (Data != null && typeof(T).GetInterfaces().Contains(typeof(IDisposable)))
        {
            ((IDisposable)Data).Dispose();
        }
    }
}