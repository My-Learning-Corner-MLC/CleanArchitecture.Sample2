using System.Net;
using FluentValidation.Results;
using Sample2.Application.Common.Constants;

namespace Sample2.Application.Common.Exceptions;

public class ValidationException : CustomException
{
    public ValidationException() : base(
        errorMessage: ExceptionConst.ErrorMessages.VALIDATION_FAILURES,
        statusCode: HttpStatusCode.BadRequest
    )
    { }

    public ValidationException(string errorDescription) : base(
        errorMessage: ExceptionConst.ErrorMessages.VALIDATION_FAILURES,
        errorDescription,
        statusCode: HttpStatusCode.BadRequest
    )
    { }

    public ValidationException(IEnumerable<ValidationFailure> failures) : base(
        errorMessage: ExceptionConst.ErrorMessages.VALIDATION_FAILURES,
        errorDescription: failures.Select(f => f.ErrorMessage).FirstOrDefault(),
        statusCode: HttpStatusCode.BadRequest
    )
    { }
}