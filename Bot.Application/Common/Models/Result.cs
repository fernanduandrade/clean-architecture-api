namespace Bot.Application.Common.Models;

public class Result
{
    internal Result(bool succeded, IEnumerable<string> errors)
    {
        Succeded = succeded;
        errors = errors.ToArray();
    }

    public bool Succeded { get; set; }
    public string[] Erros { get; set; }
    public static Result Success()
    {
        return new Result(true, Array.Empty<string>());
    }
    public static Result Failure(IEnumerable<string> errors)
    {
        return new Result(false, errors);
    }
}
