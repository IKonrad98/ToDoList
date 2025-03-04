public class JwtOptions
{
    public string PrivateKey { get; set; }
    public string PublicKey { get; set; }
    public int ExpiresHours { get; set; }
}