using MediatR;

namespace EducationSubscription.Core.Primitives;

/// <summary>
/// Represents a expressive result for domain layer.
/// </summary>
public class Result
{
    public Result(bool isSuccess, Error error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }
    public bool IsSuccess { get; set; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; set; }
    /// <summary>
    /// Creates a failure result.
    /// </summary>
    /// <param name="error"></param>
    /// <returns></returns>
    public static Result Fail(Error error) => new(false, error);
    /// <summary>
    /// Creates a failure result.
    /// </summary>
    /// <param name="error"></param>
    /// <returns></returns>
    public static Result<TValue> Fail<TValue>(Error error) => new(false, error, default!);
    /// <summary>
    /// Creates a successful result.
    /// </summary>
    /// <returns></returns>
    public static Result Ok() => new(true, Error.None);
    /// <summary>
    /// Creates a successful result.
    /// </summary>
    /// <returns></returns>
    public static Result<TValue> Ok<TValue>(TValue value) => new(true, Error.None, value);
}

/// <summary>
/// Represents a expressive result for domain layer.
/// </summary>
public class Result<TValue> : Result, IRequest
{
    public TValue Value { get; set; }
    public Result(bool isSuccess, Error error, TValue value) : base(isSuccess, error)
    {
        Value = value;
    }
}