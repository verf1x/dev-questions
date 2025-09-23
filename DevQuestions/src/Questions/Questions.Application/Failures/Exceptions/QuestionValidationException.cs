using Shared;
using Shared.Exceptions;

namespace Questions.Application.Failures.Exceptions;

public class QuestionValidationException : BadRequestException
{
    public QuestionValidationException(Error[] errors)
        : base(errors)
    {
    }
}