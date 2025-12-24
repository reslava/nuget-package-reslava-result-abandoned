namespace REslava.Result;
using System.Diagnostics.CodeAnalysis;

public class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public List<Error> Errors = [Error.None];
    public SuccessType SuccessType { get; } = SuccessType.Success;

    internal Result (bool isSuccess, Error error, SuccessType successType = SuccessType.Success)
    {
        if (isSuccess && error != Error.None ||
            !isSuccess && error == Error.None)
        {
            throw new ArgumentException ("Invalid error", nameof (error));
        }

        IsSuccess = isSuccess;
        Errors = [error];
        SuccessType = successType;
    }
    public Result (bool isSuccess, List<Error> errors)
    {
        ArgumentNullException.ThrowIfNull (errors);
        if (errors.Count == 0)
            throw new ArgumentException ("Cannot create a Result from an empty collection of errors. _" +
                "Provide at least one error.", nameof (errors));
        if (isSuccess && errors[0] != Error.None ||
            !isSuccess && errors[0] == Error.None)
        {
            throw new ArgumentException ("Invalid error", nameof (errors));
        }

        IsSuccess = isSuccess;
        Errors = errors;
    }
    public Error Error => Errors[0];

    public static Result Success () => new (true, Error.None);
    public static Result Success (SuccessType successType) => new (true, Error.None, successType);

    public static Result Failure (Error error) => new (false, error);

    public static Result<TValue> Success<TValue> (TValue value) =>
        new (value, true, Error.None);
    public static Result<TValue> Success<TValue> (TValue value, SuccessType successType) =>
        new (value, true, Error.None, successType);

    public static Result<TValue> Failure<TValue> (Error error) =>
        new (default, false, error);
}

public class Result<TValue> : Result
{
    private readonly TValue? _value;

    internal Result (TValue? value, bool isSuccess, Error error, SuccessType successType = SuccessType.Success)
        : base (isSuccess, error, successType)
    {
        _value = value;
    }

    internal Result (TValue? value, bool isSuccess, List<Error> errors, SuccessType successType = SuccessType.Success)
        : base (isSuccess, errors)
    {
        _value = value;
    }

    [NotNull]
    public TValue Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException ("The value of a failure result can't be accessed.");

    public static implicit operator Result<TValue> (TValue? value) =>
        value is not null ? Success (value) : Failure<TValue> (Error.NullValue());

    public static Result<TValue> ValidationFailure (Error error) =>
        new (default, false, error);
}