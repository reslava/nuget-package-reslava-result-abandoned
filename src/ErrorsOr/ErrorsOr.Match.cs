namespace ErrorsOr;

public readonly partial record struct ErrorsOr<TValue> : IErrorsOr<TValue>
{    
    public TNextValue Match<TNextValue> (Func<TValue, TNextValue> onValue, Func<List<Error>, TNextValue> onError)
    {
        if (IsError)
            return onError (Errors);

        return onValue (Value);
    }

    public async Task<TNextValue> MatchAsync<TNextValue> (Func<TValue, Task<TNextValue>> onValue, Func<List<Error>, Task<TNextValue>> onError)
    {
        if (IsError)
            return await onError (Errors).ConfigureAwait(false);

        return await onValue (Value).ConfigureAwait (false);
    }
}