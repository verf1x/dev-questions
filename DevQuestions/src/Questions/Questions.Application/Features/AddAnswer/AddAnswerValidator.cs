using FluentValidation;

namespace Questions.Application.Features.AddAnswer;

public class AddAnswerValidator : AbstractValidator<AddAnswerCommand>
{
    public AddAnswerValidator()
    {
        RuleFor(x => x.AddAnswerDto.Text)
            .NotEmpty().WithMessage("Text is required.")
            .MaximumLength(5000).WithMessage("Text must not exceed 5000 characters.");

        RuleFor(x => x.AddAnswerDto.UserId)
            .NotEqual(Guid.Empty).WithMessage("UserId is required.");
    }
}