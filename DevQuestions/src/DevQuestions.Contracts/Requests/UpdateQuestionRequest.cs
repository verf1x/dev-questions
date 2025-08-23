namespace DevQuestions.Contracts.Requests;

public record UpdateQuestionRequest(string Title, string Body, Guid[] TagIds);