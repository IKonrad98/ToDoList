using AutoMapper;
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

        builder.Services.AddTransient<IPasswordEncryptionHelper, PasswordEncryptionHelper>();

        builder.Services.AddAutoMapper(typeof(ToDoApiMappingProfile));

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        var mapper = app.Services.GetService<IMapper>();
        try
        {
            mapper.ConfigurationProvider.AssertConfigurationIsValid();
            Console.WriteLine("AutoMapper configuration is valid.");
        }
        catch (AutoMapperConfigurationException ex)
        {
            Console.WriteLine("AutoMapper configuration is invalid: " + ex.Message);
            throw;
        }

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