using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ToDoApi.Models;
using ToDoApi.Services.ServicesInterface;

namespace ToDoApi.Controllers;

[Route("todo")]
[ApiController]
public class ToDoItemController : ControllerBase
{
    private readonly IToDoItemService _service;

    public ToDoItemController(IToDoItemService toDoItemService)
    {
        _service = toDoItemService;
    }

    [HttpPost("create")]
    public async Task<ActionResult<ToDoItemModel>> CreateAsync(
        [FromBody] CreateToDoItemModel createToDoItemModel,
        CancellationToken cancellationToken
        )
    {
        var toDoItem = await _service.CreateAsync(createToDoItemModel, cancellationToken);

        return Ok(toDoItem);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ToDoItemModel>> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        var toDoItem = await _service.GetByIdAsync(id, cancellationToken);
        if (toDoItem is null)
        {
            return NotFound();
        }
        return Ok(toDoItem);
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult> UpdateAsync(
        [FromRoute] Guid id,
        [FromBody] JsonPatchDocument<UpdateToDoItemModel> model,
        CancellationToken cancellationToken
        )
    {
        var toDoItem = await _service.UpdateAsync(model, id, cancellationToken);
        return Ok(toDoItem);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        await _service.DeleteAsync(id, cancellationToken);
        return Ok();
    }
}