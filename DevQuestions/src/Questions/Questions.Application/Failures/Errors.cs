using Shared;

namespace Questions.Application.Failures;

public class Errors
{
    public static class General
    {
        public static Error NotFound(Guid id)
            => Error.Failure("record.not.found", $"Record with ID {id} not found.");
    }

    public static class Questions
    {
        public static Error TooManyQuestions()
            => Error.Failure(
                "questions.too.many",
                "You have reached the maximum number of questions allowed.");

        public static ErrorsList NotEnoughRating()
            => Error.Failure(
                "not.enough.rating",
                "User does not have enough rating to perform this action.");
    }
}