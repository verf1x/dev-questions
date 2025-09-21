using Shared;
using Shared.Exceptions;

namespace Questions.Application.Failures.Exceptions;

public class QuestionNotFoundException : NotFoundException
{
    public QuestionNotFoundException(Error[] errors)
        : base(errors)
    {
    }
}