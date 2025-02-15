using Microsoft.AspNetCore.Mvc;
using ToDoApi.Models;
using ToDoApi.Services.ServicesInterface;

namespace ToDoApi.Controllers;

[ApiController]
[Route("users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateUser(
        [FromBody] CreateUserModel model,
        CancellationToken cancellationToken
        )
    {
        var user = await _userService.CreateAsync(model, cancellationToken);
        return Ok(user);
    }

    [HttpGet("{email}")]
    public async Task<IActionResult> GetByEmail(
        string email,
        CancellationToken cancellationToken)
    {
        var user = await _userService.GetByEmailAsync(email, cancellationToken);
        return Ok(user);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var user = await _userService.GetByIdAsync(id, cancellationToken);
        return Ok(user);
    }
}