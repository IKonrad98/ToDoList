namespace ToDoApi.Data.Entities;

public class PasswordEntity : BaseEntity
{
    public string Hash { get; set; }

    public byte[] Salt { get; set; }
}