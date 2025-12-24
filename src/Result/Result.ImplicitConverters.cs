namespace Result;

public readonly partial record struct Result<TValue> : IResult<TValue>
{
    public static implicit operator Result<TValue> (TValue value)
    {
        return new Result<TValue> (value);
    }
    
    public static implicit operator Result<TValue> (Error error)
    {
        return new Result<TValue> (error);
    }

    public static implicit operator Result<TValue> (List<Error> errors)
    {
        return new Result<TValue> (errors);
    }

    public static implicit operator Result<TValue> (Error[] errors)
    {
        ArgumentNullException.ThrowIfNull (errors);

        return new Result<TValue> ((List<Error>)[.. errors]);
    }    
}