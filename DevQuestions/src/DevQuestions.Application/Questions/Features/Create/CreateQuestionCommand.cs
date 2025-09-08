using DevQuestions.Application.Abstractions;
using DevQuestions.Contracts.Questions;

namespace DevQuestions.Application.Questions.Features.Create;

public record CreateQuestionCommand(CreateQuestionDto CreateQuestionDto) : ICommand;