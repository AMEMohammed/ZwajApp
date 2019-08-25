using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ZwajApp.Api.Data;
using ZwajApp.Api.Dtos;
using ZwajApp.Api.Models;

namespace ZwajApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAtuthRepository _repo;
        private readonly IConfiguration _config;

        public AuthController(IAtuthRepository repo, IConfiguration config)
        {
           _config = config;
            _repo = repo;
        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            //validation
            userForRegisterDto.UserName = userForRegisterDto.UserName.ToLower();
            if (await _repo.UserExists(userForRegisterDto.UserName)) return BadRequest("هذا المستخدم مسجل من قبل");

            var userToCreate = new User
            {
                UserName = userForRegisterDto.UserName

            };
            var CretedUser = await _repo.Register(userToCreate, userForRegisterDto.Password);
            return StatusCode(201);


        }
        [HttpPost("login")]
        public async Task<ActionResult> Login(UserForLoginDto userForLoginDto, byte[] encoding)
        {
            var userfromRepo = await _repo.Login(userForLoginDto.username.ToLower(), userForLoginDto.password);
            if (userfromRepo == null) return Unauthorized();
            var calims = new[]{
              new Claim(ClaimTypes.NameIdentifier,userfromRepo.id.ToString()),
              new Claim(ClaimTypes.Name,userfromRepo.UserName),

          };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
            var creds=new SigningCredentials(key,SecurityAlgorithms.HmacSha512);
            var tokenDescipror=new SecurityTokenDescriptor{
                Subject=new ClaimsIdentity(calims),
                Expires=DateTime.Now.AddDays(1),
                SigningCredentials=creds
            };
            var tokenHandler=new JwtSecurityTokenHandler();
            var token=tokenHandler.CreateToken(tokenDescipror);
            return Ok(new{
                token=tokenHandler.WriteToken(token)
            });

         }
    }
}