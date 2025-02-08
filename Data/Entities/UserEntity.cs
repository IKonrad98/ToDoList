namespace ToDoApi.Data.Entities;

public class UserEntity : BaseEntity
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }

    public List<ToDoItemEntity> TodoItems { get; set; }
}