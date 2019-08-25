using System.Threading.Tasks;
using ZwajApp.Api.Models;

namespace ZwajApp.Api.Data
{
    public interface IAtuthRepository
    {
         Task<User>Register (User user,string password);
         Task<User>Login(string username,string password);
         Task<bool>UserExists(string username);
    }
}