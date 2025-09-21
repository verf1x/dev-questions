using DevQuestions.Contracts.Questions.Dtos;
using Shared.Abstractions;

namespace Questions.Application.Features.GetQuestionsWithFilters;

public record GetQuestionsWithFiltersQuery(GetQuestionsDto Dto) : IQuery;