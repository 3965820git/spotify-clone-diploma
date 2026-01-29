using SpotifyClone.Shared.BuildingBlocks.Application.Errors;

namespace SpotifyClone.Shared.BuildingBlocks.Application.Results;

public class Result : IResult
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error[] Errors { get; }

    protected internal Result(bool isSuccess, params Error[] errors)
    {
        if (isSuccess && errors.Length > 0)
        {
            errors = [];
        }
        else if (!isSuccess && errors.Length == 0)
        {
            errors = new[] { CommonErrors.Unknown };
        }

        IsSuccess = isSuccess;
        Errors = errors;
    }

    public static Result Success()
        => new Result(true);

    public static Result<TValue> Success<TValue>(TValue value)
        => new Result<TValue>(value, true);

    public static Result Failure(params Error[] errors)
        => new Result(false, errors ?? new[] { CommonErrors.Unknown });

    public static Result<TValue> Failure<TValue>(params Error[] errors)
        => new Result<TValue>(default!, false, errors ?? new[] { CommonErrors.Unknown });
}

public class Result<TValue> : Result
{
    public TValue Value { get; }

    protected internal Result(TValue value, bool isSuccess, params Error[] errors)
        : base(isSuccess, errors)
    {
        if (isSuccess && value is null)
        {
            throw new InvalidOperationException("Successful result must have a value.");
        }

        Value = value;
    }

    public static implicit operator Result<TValue>(TValue value)
        => new Result<TValue>(value, true);
}

public interface IResult
{
    bool IsSuccess { get; }
    bool IsFailure => !IsSuccess;
    Error[] Errors { get; }
}
