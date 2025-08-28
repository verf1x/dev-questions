namespace DevQuestions.Contracts.Questions;

public record CreateQuestionRequest(string Title, string Text, Guid UserId, Guid[] TagIds);