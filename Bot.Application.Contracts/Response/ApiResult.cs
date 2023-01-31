namespace Bot.Application.Contracts.Response;

public class ApiResult<T>
{
    public bool Successed { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }
    public List<string> Errors { get; set; } 

    public ApiResult() { }
    public ApiResult(T data, string message)
    {
        Data = data;
        Message = message;
        Successed = true;
    }
    public ApiResult(string message)
    {
        Message= message;
        Successed = false;
    }

    public static ApiResult<T> Success()
    {
        var result = new ApiResult<T> { Successed = true };
        return result;
    }

    public static ApiResult<T> Success(string message)
    {
        var result = new ApiResult<T> { Successed = true, Message = message };
        return result;
    }

    public static ApiResult<T> Success(string message, T data)
    {
        var result = new ApiResult<T> { Successed = true, Message = message, Data = data };
        return result;
    }

    public static ApiResult<T> Fail()
    {
        var result = new ApiResult<T> { Successed = false };
        return result;
    }

    public static ApiResult<T> Fail(string message)
    {
        var result = new ApiResult<T> { Successed = false, Message = message };
        return result;
    }

    public static ApiResult<T> Fail(string message, List<string> errors)
    {
        var result = new ApiResult<T> { Successed = false, Message = message, Errors = errors };
        return result;
    }
    public static ApiResult<T> Fail(List<string> errors)
    {
        var result = new ApiResult<T> { Successed = false, Errors = errors };
        return result;
    }

    public override string ToString()
    {
        return Successed ? Message : Errors == null || Errors.Count == 0 ? Message : $"{Message} : {string.Join(",", Errors)}";
    }
}

