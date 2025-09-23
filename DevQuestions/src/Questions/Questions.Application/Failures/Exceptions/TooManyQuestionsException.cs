using Shared.Exceptions;

namespace Questions.Application.Failures.Exceptions;

public class TooManyQuestionsException : BadRequestException
{
    public TooManyQuestionsException()
        : base([Errors.Questions.TooManyQuestions()])
    {
    }
}