using CSharpFunctionalExtensions;
using Questions.Domain;
using Shared;

namespace Questions.Application.FullTextSearch;

public interface ISearchEngine
{
    Task<List<Guid>> SearchAsync(string query);

    Task<UnitResult<ErrorsList>> IndexQuestionAsync(Question question);
}