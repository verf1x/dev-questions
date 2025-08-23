namespace DevQuestions.Contracts.Requests;

public record AddAnswerRequest(Guid UserId, string Text);