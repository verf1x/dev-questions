using CSharpFunctionalExtensions;
using DevQuestions.Application.FullTextSearch;
using DevQuestions.Domain.Questions;
using Shared;

namespace DevQuestions.Infrastructure.ElasticSearch;

public class ElasticSearchEngine : ISearchEngine
{
    public Task<List<Guid>> SearchAsync(string query) => throw new NotImplementedException();

    public async Task<UnitResult<ErrorsList>> IndexQuestionAsync(Question question)
    {
        try
        {
            // _elastic.Search();
        }
        catch (Exception ex)
        {
            return Error.Failure("search.error", ex.Message).ToErrors();
        }

        return UnitResult.Success<ErrorsList>();
    }
}