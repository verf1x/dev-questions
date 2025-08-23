namespace DevQuestions.Contracts.Requests;

public record GetQuestionsRequest(string Search, Guid[] TagIds, int Page, int Limit);