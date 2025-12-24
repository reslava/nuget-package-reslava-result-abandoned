using System.Diagnostics.CodeAnalysis;

namespace Result;

public readonly partial record struct Result<TValue> : IResult<TValue>
{    
    private readonly TValue? _value = default;
    private readonly List<Error>? _errors = null;
    private readonly SuccessType _successType = SuccessType.Success;

    public SuccessType SuccessType
    {
        get { return _successType; } 
    }

    public Result ()
    {        
        throw new InvalidOperationException ("Use Factory methods instead of default constructor.");
    }
    private Result (Error error)
    {
        _errors = [error];
    }

    private Result (List<Error> errors)
    {
        ArgumentNullException.ThrowIfNull (errors);
        
        if (errors.Count == 0)        
            throw new ArgumentException ("Cannot create an ErrorOr<TValue> from an empty collection of errors. Provide at least one error.", nameof (errors));        

        _errors = errors;
    }

    private Result (TValue value)
    {
        ArgumentNullException.ThrowIfNull(value);
        
        _value = value;
    }

    [MemberNotNullWhen (true, nameof (_errors))]
    [MemberNotNullWhen (true, nameof (Errors))]
    [MemberNotNullWhen (false, nameof (Value))]
    [MemberNotNullWhen (false, nameof (_value))]
    public bool IsError => _errors is not null;

    public List<Error> Errors => IsError ? _errors : throw new InvalidOperationException ("No errors found. Check IsError before.");

    public TValue Value
    {
        get
        {
            if (IsError)        
                throw new InvalidOperationException ("Errors has ocurred. Check IsError before.");            

            return _value;
        }
    }

    public Error FirstError 
    {
        get
        {
            if (!IsError)
                throw new InvalidOperationException ("No errors found. Check IsError before.");

            return Errors[0];
        }
    }
}