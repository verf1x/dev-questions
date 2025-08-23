using DevQuestions.Contracts.Requests;
using Microsoft.AspNetCore.Mvc;

namespace DevQuestions.Presenters;

[ApiController]
[Route("[controller]")]
public class QuestionsController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateAsync(
        [FromBody] CreateQuestionRequest request,
        CancellationToken cancellationToken = default)
    {
        return Ok("Question created");
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync(
        [FromQuery] GetQuestionsRequest request,
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
        [FromBody] UpdateQuestionRequest request,
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

    [HttpGet("{questionId:guid}/answers")]
    public async Task<IActionResult> AddAnswersAsync(
        [FromRoute] Guid questionId,
        [FromBody] AddAnswerRequest request,
        CancellationToken cancellationToken = default)
    {
        return Ok("Answer added");
    }
}