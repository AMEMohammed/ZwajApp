namespace ZwajApp.Api.Models
{
    public class User
    {
    public int id { get; set; }
   public string UserName { get; set; }
   public byte[] PassWordHash { get; set; }
public byte[] passWordSalt { get; set; }
    }
}