using System.Net;
using Sample2.Application.Constants;

namespace Sample2.Application.Common.Exceptions;

public class ConflictException : CustomException
{
    public ConflictException() : base(
        errorMessage: ExceptionConst.ErrorMessages.RESOURCE_CONFLICT, 
        statusCode: HttpStatusCode.Conflict
    ) 
    { }

    public ConflictException(string errorMessage) : base(
        errorMessage, 
        statusCode: HttpStatusCode.Conflict
    ) 
    { }

    public ConflictException(string errorMessage, string errorDescription) : base(
        errorMessage, 
        errorDescription, 
        statusCode: HttpStatusCode.Conflict
    )
    { }
}