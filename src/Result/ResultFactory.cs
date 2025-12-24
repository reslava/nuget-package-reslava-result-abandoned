namespace Result;

public static class ResultFactory
{
    public static Result<TValue> From<TValue> (Error error)
    {
        return error;
    }

    public static Result<TValue> From<TValue> (List<Error> errors)
    {
        return errors;
    }

    //public static Result<TValue> From<TValue> (Error[] errors)
    //{
    //    return errors;
    //}

    public static Result<TValue> From<TValue> (IEnumerable<Error> errors)
    {
        return errors.ToList<Error>();
    }

    public static Result<TValue> From <TValue>(TValue value)
    {
        return value;
    }    
}