using System;
using System.Collections.Generic;
using System.Text;

namespace ErrorsOr;

public readonly record struct Error
{
    public ErrorType Type { get; }
    public string Code { get; }
    public string Message { get; }
    private Error (ErrorType type, string code, string message)
    {
        Type = type;
        Code = code;
        Message = message;
    }

    public static Error Unexpected (
        string code = "General.Unexpected", 
        string message = "An unexpected error has ocurred")
    {
        return new (ErrorType.Unexpected, code, message);
    }
    public static Error NoErrors (string code = "Success.NoErrors", string message = "No errors has been found")
    {
        return new Error (ErrorType.NoErrors, code, message);
    }
}