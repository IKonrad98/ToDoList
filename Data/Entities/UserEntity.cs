namespace ToDoApi.Data.Entities;

public class UserEntity : BaseEntity
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public DateTime CreateUser { get; set; }

    public List<ToDoItemEntity> ToDoItems { get; set; }
}