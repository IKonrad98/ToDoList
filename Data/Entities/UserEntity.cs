namespace ToDoApi.Data.Entities;

public class UserEntity : BaseEntity
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public DateTime CreateUser { get; set; } = DateTime.UtcNow;

    public Guid? PasswordId { get; set; }
    public PasswordEntity? Password { get; set; }

    public List<ToDoItemEntity> ToDoItems { get; set; }
}