using News.Services.ServicesInterface;
using ToDoApi.Infrastructure.InfrastructureInterfaces;
using ToDoApi.Models;
using ToDoApi.Services.ServicesInterface;

namespace News.Services;

public class IdentityService : IIdentityService
{
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;
    private readonly IPasswordEncryptionHelper _passwordEncryptionHelper;

    public IdentityService(
        IUserService userService,
        ITokenService tokenService,
        IPasswordEncryptionHelper passwordEncryptionHelper
        )
    {
        _userService = userService;
        _tokenService = tokenService;
        _passwordEncryptionHelper = passwordEncryptionHelper;
    }

    public async Task<TokensPair> LoginAsync(
        LoginUserModel login,
        CancellationToken cancellationToken
        )
    {
        var user = await _userService.LoginAsync(login, cancellationToken);
        var token = _tokenService.GenerateTokensPairAsync(user, cancellationToken);
        await _tokenService.SaveTokenAsync(token.Refresh, cancellationToken);

        return token;
    }

    public async Task<TokensPair> RegisterAsync(
        CreateUserModel newUser,
        CancellationToken cancellationToken
        )
    {
        var user = await _userService.CreateAsync(newUser, cancellationToken);
        var token = _tokenService.GenerateTokensPairAsync(user, cancellationToken);
        await _tokenService.SaveTokenAsync(token.Refresh, cancellationToken);

        return token;
    }

    public async Task<bool> LogOutAsync(string refresh, CancellationToken cancellation)
    {
        await _tokenService.DeleteTokenAsync(refresh, cancellation);
        await _tokenService.SaveTokenAsync(refresh, cancellation);

        return true;
    }
}