using CSharpFunctionalExtensions;
using DevQuestions.Domain.Questions;
using Shared;

namespace DevQuestions.Application.FullTextSearch;

public interface ISearchEngine
{
    Task<List<Guid>> SearchAsync(string query);

    Task<UnitResult<ErrorsList>> IndexQuestionAsync(Question question);
}