using Microsoft.EntityFrameworkCore;
using ToDoApi.Data;
using ToDoApi.DataAccess;
using ToDoApi.DataAccess.RepoInterfaces;
using ToDoApi.Infrastructure;
using ToDoApi.Services;
using ToDoApi.Services.ServicesInterface;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<ToDoApiDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddTransient<IToDoItemRepo, ToDoItemRepo>();
        builder.Services.AddTransient<IToDoItemService, ToDoItemService>();
        builder.Services.AddTransient<IUserRepo, UserRepo>();
        builder.Services.AddTransient<IUserService, UserService>();
        builder.Services.AddTransient<IPasswordRepo, PasswordRepo>();
        builder.Services.AddTransient<IPasswordEncryptionHelper, PasswordEncryptionHelper>();

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

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
    }
}