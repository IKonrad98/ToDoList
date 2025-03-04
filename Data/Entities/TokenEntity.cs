namespace ToDoApi.Data.Entities;

public class TokenEntity : BaseEntity
{
    public string Refresh { get; set; }

    public Guid UserId { get; set; }

    public UserEntity User { get; set; }
}