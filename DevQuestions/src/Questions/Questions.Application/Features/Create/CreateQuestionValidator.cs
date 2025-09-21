using DevQuestions.Contracts.Questions.Dtos;
using FluentValidation;

namespace Questions.Application.Features.Create;

public class CreateQuestionValidator : AbstractValidator<CreateQuestionDto>
{
    public CreateQuestionValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");

        RuleFor(x => x.Text)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(5000).WithMessage("Text must not exceed 200 characters.");

        RuleFor(x => x.UserId)
            .NotEqual(Guid.Empty).WithMessage("UserId is required.");
    }
}