using DevQuestions.Application.Exceptions;
using Shared;

namespace DevQuestions.Application.Questions.Failures.Exceptions;

public class QuestionValidationException : BadRequestException
{
    public QuestionValidationException(Error[] errors)
        : base(errors)
    {
    }
}