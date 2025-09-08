using DevQuestions.Application.Abstractions;
using DevQuestions.Application.Questions.Features.AddAnswer;
using DevQuestions.Application.Questions.Features.Create;
using DevQuestions.Contracts.Questions;
using DevQuestions.Presenters.ResponseExtensions;
using Microsoft.AspNetCore.Mvc;

namespace DevQuestions.Presenters.Questions;

[ApiController]
[Route("[controller]")]
public class QuestionsController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateAsync(
        [FromBody] CreateQuestionDto dto,
        [FromServices] ICommandHandler<CreateQuestionCommand, Guid> handler,
        CancellationToken cancellationToken = default)
    {
        var command = new CreateQuestionCommand(dto);

        var result = await handler.HandleAsync(command, cancellationToken);
        return result.IsFailure ? result.Error.ToResponse() : Ok(result.Value);
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync(
        [FromQuery] GetQuestionsDto dto,
        CancellationToken cancellationToken = default)
    {
        return Ok("Question get");
    }

    [HttpGet("{questionId:guid}")]
    public async Task<IActionResult> GetByIdAsync(
        [FromRoute] Guid questionId,
        CancellationToken cancellationToken = default)
    {
        return Ok("Question get");
    }

    [HttpPut("{questionId:guid}")]
    public async Task<IActionResult> UpdateAsync(
        [FromRoute] Guid questionId,
        [FromBody] UpdateQuestionDto dto,
        CancellationToken cancellationToken = default)
    {
        return Ok("Question updated");
    }

    [HttpDelete("{questionId:guid}")]
    public async Task<IActionResult> DeleteAsync(
        [FromRoute] Guid questionId,
        CancellationToken cancellationToken = default)
    {
        return Ok("Question deleted");
    }

    [HttpPut("{questionId:guid}/correct_answer")]
    public async Task<IActionResult> SelectSolution(
        [FromRoute] Guid questionId,
        [FromQuery] Guid answerId,
        CancellationToken cancellationToken = default)
    {
        return Ok("Solution selected");
    }

    [HttpPost("{questionId:guid}/answers")]
    public async Task<IActionResult> AddAnswerAsync(
        [FromRoute] Guid questionId,
        [FromBody] AddAnswerDto dto,
        [FromServices] ICommandHandler<AddAnswerCommand, Guid> handler,
        CancellationToken cancellationToken = default)
    {
        var command = new AddAnswerCommand(questionId, dto);

        var result = await handler.HandleAsync(command, cancellationToken);
        return result.IsFailure ? result.Error.ToResponse() : Ok(result.Value);
    }
}