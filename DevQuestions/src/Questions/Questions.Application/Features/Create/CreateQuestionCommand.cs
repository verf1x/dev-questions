using Questions.Contracts.Dtos;
using Shared.Abstractions;

namespace Questions.Application.Features.Create;

public record CreateQuestionCommand(CreateQuestionDto CreateQuestionDto) : IValidation, ILogging;