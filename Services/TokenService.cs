using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using ToDoApi.Data.Entities;
using ToDoApi.DataAccess.Repos.RepoInterfaces;
using ToDoApi.Infrastructure.InfrastructureInterfaces;
using ToDoApi.Models;
using ToDoApi.Services.ServicesInterface;

namespace ToDoApi.Services;

public class TokenService : ITokenService
{
    private readonly JwtOptions _options;
    private readonly ITokenRepo _tokenRepo;
    private readonly IUserService _userService;
    private readonly ITokenHelper _tokenHelper;

    public TokenService(
        IOptions<JwtOptions> options,
        ITokenRepo tokenRepo,
        IUserService userService,
        ITokenHelper tokenHelper
        )
    {
        _options = options.Value;
        _tokenRepo = tokenRepo;
        _userService = userService;
        _tokenHelper = tokenHelper;
    }

    public TokensPair GenerateTokensPairAsync(
        UserModel user,
        CancellationToken cancellationToken
        )
    {
        var accessToken = GenerateAccessToken(user, cancellationToken);
        var refreshToken = GenerateRefreshToken(user, cancellationToken);

        return new TokensPair
        {
            Access = accessToken,
            Refresh = refreshToken
        };
    }

    public async Task<TokensPair> RefreshTokensPairAsync(
        string refresh,
        CancellationToken cancellationToken
        )
    {
        var isTokenValid = VerifyToken(refresh);

        if (!isTokenValid)
        {
            throw new Exception("Token is not valid");
        }

        var oldRefreshId = _tokenHelper.GetTokenIdFromRefresh(refresh);
        var userId = _tokenHelper.GetUserIdFromRefresh(refresh);

        var user = await _userService.GetByIdAsync(userId, cancellationToken);
        var newTokens = GenerateTokensPairAsync(user, cancellationToken);

        await DeleteTokenAsync(refresh, cancellationToken);
        await SaveTokenAsync(newTokens.Refresh, cancellationToken);

        return newTokens;
    }

    public async Task DeleteTokenAsync(string refresh, CancellationToken cancellationToken)
    {
        var isTokenValid = VerifyToken(refresh);

        if (!isTokenValid)
        {
            throw new Exception("Token is not valid");
        }

        var tokenId = _tokenHelper.GetTokenIdFromRefresh(refresh);
        await _tokenRepo.DeleteAsync(tokenId, cancellationToken);
    }

    public async Task SaveTokenAsync(string refresh, CancellationToken cancellationToken)
    {
        var userId = _tokenHelper.GetUserIdFromRefresh(refresh);
        var refreshId = _tokenHelper.GetTokenIdFromRefresh(refresh);

        var refreshEntity = new TokenEntity
        {
            Id = refreshId,
            UserId = userId,
            Refresh = refresh
        };

        await _tokenRepo.CreateAsync(refreshEntity, cancellationToken);
    }

    private string GenerateAccessToken(
        UserModel user,
        CancellationToken cancellationToken
        )
    {
        var claims = new[]
        {
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim("login", user.UserName),
        new Claim("userId", user.Id.ToString()),
        new Claim("email", user.Email)
        };

        var rsa = RSA.Create();
        rsa.ImportFromPem(_options.PrivateKey.ToCharArray());

        var signingCredentials = new SigningCredentials(
            new RsaSecurityKey(rsa),
            SecurityAlgorithms.RsaSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            signingCredentials: signingCredentials,
            expires: DateTime.UtcNow.AddHours(_options.ExpiresHours));

        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

        return tokenValue;
    }

    private string GenerateRefreshToken(
        UserModel user,
        CancellationToken cancellationToken
        )
    {
        var claims = new[]
        {
        new Claim("userId", user.Id.ToString()),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var rsa = RSA.Create();
        rsa.ImportFromPem(_options.PrivateKey.ToCharArray());

        var signingCredentials = new SigningCredentials(
            new RsaSecurityKey(rsa),
            SecurityAlgorithms.RsaSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            signingCredentials: signingCredentials,
            expires: DateTime.UtcNow.AddDays(7));

        var refreshToken = new JwtSecurityTokenHandler().WriteToken(token);

        return refreshToken;
    }

    private bool VerifyToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var rsa = RSA.Create();
        rsa.ImportFromPem(_options.PublicKey.ToCharArray());

        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new RsaSecurityKey(rsa),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true
        };

        try
        {
            tokenHandler.ValidateToken(token, validationParameters, out _);
            return true;
        }
        catch
        {
            return false;
        }
    }
}