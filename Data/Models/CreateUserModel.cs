namespace ToDoApi.Models;

public class CreateUserModel
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime CreateUser { get; set; } = DateTime.Now;
}