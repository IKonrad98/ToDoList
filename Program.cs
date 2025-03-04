using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using News.Services;
using News.Services.ServicesInterface;
using System.Text;
using ToDoApi.DataAccess;
using ToDoApi.DataAccess.Repos;
using ToDoApi.DataAccess.Repos.RepoInterfaces;
using ToDoApi.Infrastructure;
using ToDoApi.Infrastructure.InfrastructureInterfaces;
using ToDoApi.Services;
using ToDoApi.Services.ServicesInterface;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));

var jwtOptions = builder.Configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.PrivateKey))
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddDbContext<ToDoApiDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient<IToDoItemRepo, ToDoItemRepo>();
builder.Services.AddTransient<IToDoItemService, ToDoItemService>();
builder.Services.AddTransient<IUserRepo, UserRepo>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IPasswordRepo, PasswordRepo>();
builder.Services.AddTransient<IPasswordEncryptionHelper, PasswordEncryptionHelper>();
builder.Services.AddTransient<ITokenRepo, TokenRepo>();
builder.Services.AddTransient<ITokenService, TokenService>();
builder.Services.AddTransient<IIdentityService, IdentityService>();
builder.Services.AddTransient<ITokenHelper, TokenHelper>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers().AddNewtonsoftJson();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();