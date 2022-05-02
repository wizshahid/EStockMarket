﻿namespace Utilities.Exceptions;

public class AppException : Exception
{
    public AppException(string message) : base(message)
    {

    }
}

public class AppValidationException : Exception
{
    public AppValidationException(List<AppError> errors)
    {
        Errors = errors;
    }

    public List<AppError> Errors { get; private set; }
}


public class AuthorizationException : AppException
{
    public AuthorizationException(string message) : base(message)
    {

    }
}