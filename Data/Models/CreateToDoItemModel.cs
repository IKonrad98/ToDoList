using ToDoApi.Data.Entities;

namespace ToDoApi.Models;

public class CreateToDoItemModel
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public DateTime? Deadline { get; set; }
    public PriorityLevel Priority { get; set; }
}