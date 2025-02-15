namespace ToDoApi.Infrastructure;

public interface IPasswordEncryptionHelper
{
    public byte[] GenerateSalt(string password);

    public string HashPassword(string password, byte[] salt);

    public bool VerifyPassword(string password, string hash, byte[] salt);
}