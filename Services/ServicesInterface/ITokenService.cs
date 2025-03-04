using ToDoApi.Models;

namespace ToDoApi.Services.ServicesInterface;

public interface ITokenService
{
    public TokensPair GenerateTokensPairAsync(UserModel user, CancellationToken cancellationToken);

    public Task<TokensPair> RefreshTokensPairAsync(string refresh, CancellationToken cancellationToken);

    Task SaveTokenAsync(string refresh, CancellationToken cancellationToken);

    Task DeleteTokenAsync(string refresh, CancellationToken cancellationToken);
}