using DevQuestions.Contracts.Questions.Dtos;
using Shared.Abstractions;

namespace Questions.Application.Features.Create;

public record CreateQuestionCommand(CreateQuestionDto CreateQuestionDto) : ICommand;