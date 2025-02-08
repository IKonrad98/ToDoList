using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoApi.Data.Entities;

namespace ToDoApi.Configurations;

public class ToDoItemConfigurations : IEntityTypeConfiguration<ToDoItemEntity>
{
    public void Configure(EntityTypeBuilder<ToDoItemEntity> builder)
    {
        builder.ToTable("ToDoItems");

        builder.HasKey(i => i.Id);

        builder.Property(i => i.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(i => i.Description)
            .HasMaxLength(500);

        builder.Property(i => i.CreateItem)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(i => i.IsCompleted)
            .HasDefaultValue(false);

        builder.HasOne(i => i.User)
            .WithMany(u => u.ToDoItems)
            .HasForeignKey(i => i.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}