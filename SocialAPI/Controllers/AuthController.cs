using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialAPI.TData;
using SocialAPI.TDto;
using SocialAPI.TEntities;
using System.Security.Cryptography;
using System.Text;

namespace SocialAPI.Controllers
{
    public class AuthController : BaseController
    {
        private readonly TDataContext _context;

        public AuthController(TDataContext context) 
        {
            _context = context;
        }

        //User Register

        [HttpPost("register")] //POST: api/auth/register
        public async Task<ActionResult<AppUser>> UserRegister(RegisterDto registerDto) 
        {
            if (await IsUserExists(registerDto.Username)) return BadRequest("Username is already exists");
            using var hmac = new HMACSHA512();
            var user = new AppUser
            {
                UserName = registerDto.Username,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return Ok(user);
        }

        [HttpPost("login")] //POST: api/auth/register
        public async Task<ActionResult<AppUser>> UserLoging(LoginDto loginDto)
        {
           var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName.ToLower() ==  loginDto.Username.ToLower());
            if (user == null) return Unauthorized("Invalid Username");
            using var hamc = new HMACSHA512(user.PasswordSalt);
            var computedHash = hamc.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
            for(int i=0; i< computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid Passoword");
            }

            return Ok(user);
        }

        private async Task<bool> IsUserExists(string username)
        {
            return await _context.Users.AnyAsync(x => x.UserName.ToLower() == username.ToLower());
        }
    }
}
