using ToDoApi.Models;

namespace News.Services.ServicesInterface;

public interface IIdentityService
{
    public Task<TokensPair> LoginAsync(LoginUserModel login, CancellationToken cancellationToken);

    public Task<TokensPair> RegisterAsync(CreateUserModel newUser, CancellationToken cancellationToken);

    public Task<bool> LogOutAsync(string refresh, CancellationToken cancellationToken);
}