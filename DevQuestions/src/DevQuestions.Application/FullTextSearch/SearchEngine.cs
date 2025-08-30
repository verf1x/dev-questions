using DevQuestions.Domain.Questions;

namespace DevQuestions.Application.FullTextSearch;

public interface ISearchEngine
{
    Task<List<Guid>> SearchAsync(string query);

    Task IndexQuestionAsync(Question question);
}