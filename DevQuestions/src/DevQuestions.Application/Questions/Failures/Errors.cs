using Shared;

namespace DevQuestions.Application.Questions.Failures;

public partial class Errors
{
    public static class Questions
    {
        public static Error TooManyQuestions()
            => Error.Failure(
                "questions.too.many",
                "You have reached the maximum number of questions allowed.");
    }
}