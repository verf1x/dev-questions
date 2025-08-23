namespace DevQuestions.Contracts.Requests;

public record CreateQuestionRequest(string Title, string Body, Guid UserId, Guid[] TagIds);