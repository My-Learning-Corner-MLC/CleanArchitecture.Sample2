using System.Net;
using Sample2.Application.Constants;

namespace Sample2.Application.Common.Exceptions;

public class NotFoundException : CustomException
{
    public NotFoundException() : base(
        errorMessage: ExceptionConst.ErrorMessages.RESOURCE_NOT_FOUND, 
        statusCode: HttpStatusCode.NotFound
    )
    { }

    public NotFoundException(string errorMessage) : base(
        errorMessage, 
        statusCode: HttpStatusCode.NotFound
    )
    { }

    public NotFoundException(string errorMessage, string errorDescription) : base(
        errorMessage,
        errorDescription,
        statusCode: HttpStatusCode.NotFound
    )
    { }
}