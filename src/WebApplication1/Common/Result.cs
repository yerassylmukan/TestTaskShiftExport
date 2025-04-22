namespace WebApplication1.Common;

public class Result
{
    public Result(bool success)
    {
        IsSuccess = success;
    }

    public Result(string errorMessage)
    {
        IsSuccess = false;
        ErrorMessage = errorMessage;
    }

    public bool IsSuccess { get; }
    public string ErrorMessage { get; }

    public static Result SuccessResult()
    {
        return new Result(true);
    }

    public static Result FailureResult(string errorMessage)
    {
        return new Result(errorMessage);
    }
}

public class Result<T> : Result
{
    public Result(T data) : base(true)
    {
        Data = data;
    }

    public Result(string errorMessage) : base(errorMessage)
    {
    }

    public T Data { get; }

    public static Result<T> SuccessResult(T data)
    {
        return new Result<T>(data);
    }

    public new static Result<T> FailureResult(string errorMessage)
    {
        return new Result<T>(errorMessage);
    }
}