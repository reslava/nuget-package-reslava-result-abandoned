namespace ErrorsOr;

public interface IErrorsOr
{
    List<Error>? Errors { get; }

    bool IsError { get; }
}

public interface IErrorsOr<out TValue> : IErrorsOr
{
    TValue Value { get; }
}