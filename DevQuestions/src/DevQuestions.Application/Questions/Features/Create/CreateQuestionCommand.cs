using DevQuestions.Application.Abstractions;
using DevQuestions.Contracts.Questions;
using DevQuestions.Contracts.Questions.Dtos;

namespace DevQuestions.Application.Questions.Features.Create;

public record CreateQuestionCommand(CreateQuestionDto CreateQuestionDto) : ICommand;