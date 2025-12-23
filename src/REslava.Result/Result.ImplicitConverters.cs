namespace Result;

public readonly partial record struct Result<TValue> : IErrorsOr<TValue>
{
    public static implicit operator Result<TValue> (TValue value)
    {
        return new Result<TValue> (value);
    }
    
    public static implicit operator Result<TValue> (Error error)
    {
        return new Result<TValue> (error);
    }
}