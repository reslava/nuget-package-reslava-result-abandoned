using System;
using System.Collections.Generic;
using System.Text;

namespace ErrorsOr;

// Error types
public enum ErrorType
{    
    Validation,
    NotFound,
    Unauthorized,
    Forbidden,
    Unexpected,
    NoErrors
}
