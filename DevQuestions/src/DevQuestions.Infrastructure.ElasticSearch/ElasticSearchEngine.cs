using DevQuestions.Application.FullTextSearch;
using DevQuestions.Domain.Questions;

namespace DevQuestions.Infrastructure.ElasticSearch;

public class ElasticSearchEngine : ISearchEngine
{
    public Task<List<Guid>> SearchAsync(string query) => throw new NotImplementedException();

    public Task IndexQuestionAsync(Question question) => throw new NotImplementedException();
}