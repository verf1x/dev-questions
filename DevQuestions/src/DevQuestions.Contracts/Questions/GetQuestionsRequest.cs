namespace DevQuestions.Contracts.Questions;

public record GetQuestionsRequest(string Search, Guid[] TagIds, int Page, int Limit);