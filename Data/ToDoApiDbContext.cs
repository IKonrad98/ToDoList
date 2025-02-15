using Microsoft.EntityFrameworkCore;
using System.Reflection;
using ToDoApi.Data.Entities;

namespace ToDoApi.Data;

public class ToDoApiDbContext : DbContext
{
    public DbSet<ToDoItemEntity> ToDoItems { get; set; }
    public DbSet<UserEntity> Users { get; set; }

    public ToDoApiDbContext(DbContextOptions<ToDoApiDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}