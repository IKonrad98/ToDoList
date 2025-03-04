using Microsoft.AspNetCore.Mvc;
using News.Services.ServicesInterface;
using ToDoApi.Models;
using ToDoApi.Services.ServicesInterface;

namespace ToDoApi.Controllers;

[ApiController]
[Route("identities")]
public class IdentitiesController : ControllerBase
{
    private readonly IIdentityService _identityService;
    private readonly ITokenService _tokenService;

    public IdentitiesController(
        IIdentityService identityService,
        ITokenService tokenService
        )
    {
        _identityService = identityService;
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(
        [FromBody] LoginUserModel login,
        CancellationToken cancellationToken
        )
    {
        var token = await _identityService.LoginAsync(login, cancellationToken);

        return Ok(token);
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync(
        [FromBody] CreateUserModel user,
        CancellationToken cancellationToken
        )
    {
        var token = await _identityService.RegisterAsync(user, cancellationToken);

        return Ok(token);
    }

    [HttpPost("logout")]
    public async Task<IActionResult> LogoutAsync(
        [FromBody] string refresh,
        CancellationToken cancellationToken
        )
    {
        var result = await _identityService.LogOutAsync(refresh, cancellationToken);

        return Ok(result);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshAsync(
        [FromBody] string refresh,
        CancellationToken cancellationToken
        )
    {
        var token = await _tokenService
            .RefreshTokensPairAsync(refresh, cancellationToken);

        return Ok(token);
    }
}