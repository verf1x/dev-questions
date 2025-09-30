using FluentValidation;

namespace Questions.Application.Features.Create;

public class CreateQuestionValidator : AbstractValidator<CreateQuestionCommand>
{
    public CreateQuestionValidator()
    {
        RuleFor(x => x.CreateQuestionDto.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");

        RuleFor(x => x.CreateQuestionDto.Text)
            .NotEmpty().WithMessage("Text is required.")
            .MaximumLength(5000).WithMessage("Text must not exceed 5000 characters.");

        RuleFor(x => x.CreateQuestionDto.UserId)
            .NotEqual(Guid.Empty).WithMessage("UserId is required.");
    }
}