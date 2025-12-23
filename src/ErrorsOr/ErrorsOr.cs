using System.Diagnostics.CodeAnalysis;

namespace ErrorsOr;

public readonly partial record struct ErrorsOr<TValue> : IErrorsOr<TValue>
{
    private readonly TValue? _value = default;
    private readonly List<Error>? _errors = null;    
    
    public ErrorsOr ()
    {
        throw new InvalidOperationException ("Default construction of ErrorsOr<TValue> is invalid. Please use provided factory methods to instantiate.");
    }
    public ErrorsOr (Error error)
    {
        _errors = [error];
    }

    private ErrorsOr (List<Error> errors)
    {
        if (errors is null)
        {
            throw new ArgumentNullException (nameof (errors));
        }

        if (errors is null || errors.Count == 0)
        {
            throw new ArgumentException ("Cannot create an ErrorOr<TValue> from an empty collection of errors. Provide at least one error.", nameof (errors));
        }

        _errors = errors;
    }

    public ErrorsOr (TValue value)
    {
        if (value is null)
        {
            throw new ArgumentNullException (nameof (value));
        }

        _value = value;
    }
    [MemberNotNullWhen (true, nameof (_errors))]
    [MemberNotNullWhen (true, nameof (Errors))]
    [MemberNotNullWhen (false, nameof (Value))]
    [MemberNotNullWhen (false, nameof (_value))]
    public bool IsError => _errors is not null;

    public List<Error> Errors => IsError ? _errors : throw new InvalidOperationException ("The Errors property cannot be accessed when no errors have been recorded. Check IsError before accessing Errors.");

    public TValue Value
    {
        get
        {
            if (IsError)
            {
                throw new InvalidOperationException ("The Value property cannot be accessed when errors have been recorded. Check IsError before accessing Value.");
            }

            return _value;
        }
    }    
}