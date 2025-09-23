using CSharpFunctionalExtensions;
using Shared;
using Shared.FullTextSearch;

namespace Infrastructure.ElasticSearch;

public class ElasticSearchEngine : ISearchProvider
{
    public Task<List<Guid>> SearchAsync(string query) => throw new NotImplementedException();

    public Task<UnitResult<ErrorsList>> IndexQuestionAsync<TEntity>(TEntity entity, string indexName) => throw new NotImplementedException();
}