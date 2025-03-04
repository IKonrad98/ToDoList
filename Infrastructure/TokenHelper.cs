using System.IdentityModel.Tokens.Jwt;
using ToDoApi.Infrastructure.InfrastructureInterfaces;

namespace ToDoApi.Infrastructure;

public class TokenHelper : ITokenHelper
{
    public Guid GetTokenIdFromRefresh(string refresh)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenHandler.ReadJwtToken(refresh);
        var tokenId = Guid.Parse(jwtToken.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Jti).Value);

        return tokenId;
    }

    public Guid GetUserIdFromRefresh(string refresh)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenHandler.ReadJwtToken(refresh);
        var userId = Guid.Parse(jwtToken.Claims.First(claim => claim.Type == "userId").Value);

        return userId;
    }
}