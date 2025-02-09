namespace ToDoApi.Data.Entities;

public class ToDoItemEntity : BaseEntity
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public bool? IsCompleted { get; set; }
    public DateTime CreateItem { get; set; } = DateTime.UtcNow;
    public DateTime? Deadline { get; set; }

    public PriorityLevel? Priority { get; set; } = PriorityLevel.Low;

    public Guid UserId { get; set; }
    public UserEntity User { get; set; }
}

public enum PriorityLevel
{
    Low = 1,
    Medium = 2,
    High = 3
}