namespace ErrorsOr;

public readonly partial record struct ErrorsOr<TValue> : IErrorsOr<TValue>
{
    public static implicit operator ErrorsOr<TValue> (TValue value)
    {
        return new ErrorsOr<TValue> (value);
    }
    
    public static implicit operator ErrorsOr<TValue> (Error error)
    {
        return new ErrorsOr<TValue> (error);
    }
}