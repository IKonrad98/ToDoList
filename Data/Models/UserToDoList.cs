using ToDoApi.Data.Entities;

namespace ToDoApi.Data.Models;

public class UserToDoList
{
    public List<ToDoItemEntity>? ToDoItemsList { get; set; }
}