using System;
using System.Threading.Tasks;
using ZwajApp.Api.Models;
using Microsoft.EntityFrameworkCore;
namespace ZwajApp.Api.Data
{
    public class AtuthRepository : IAtuthRepository
    {
        private readonly DataContext _context;
        public AtuthRepository(DataContext context)
        {
            _context = context;

        }


        public async Task<User> Login(string username, string password)
        {
            var user=await _context.Users.FirstOrDefaultAsync(x=>x.UserName==username);
            if (user==null)return null;
            if(!VerifyPasswordHash(password,user.passWordSalt,user.PassWordHash))
            return null;
           return user; 

        }

        private bool VerifyPasswordHash(string password, byte[] passWordSalt, byte[] passWordHash)
        {
             using(var hmac=new System.Security.Cryptography.HMACSHA512(passWordSalt)){
               
            var    ComputHash=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            for (int i = 0; i < ComputHash.Length; i++)
            {
                if(ComputHash[i]!=passWordHash[i])
                {
                return false;
                }
             
            }
               return true;            

        }
        }

        public async Task<User> Register(User user, string password)
        {
           byte[] passwordHash,passwordSalt;
           CreatePasswordHash(password,out passwordHash,out passwordSalt);
           user.passWordSalt=passwordSalt;
           user.PassWordHash=passwordHash;
           await _context.Users.AddAsync(user);
           await _context.SaveChangesAsync();
         return user;

        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac=new System.Security.Cryptography.HMACSHA512()){
                passwordSalt=hmac.Key;
                passwordHash=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

        }

        
          
        }

        public async Task<bool> UserExists(string username)
        {
            if (await _context.Users.AnyAsync(x=>x.UserName==username)) return true;
           
            return false;
        }
    }
}