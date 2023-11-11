using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialAPI.TData;
using SocialAPI.TDto;
using SocialAPI.TEntities;
using SocialAPI.TInterfaces;
using System.Security.Cryptography;
using System.Text;

namespace SocialAPI.Controllers
{
    public class AuthController : BaseController
    {
        private readonly TDataContext _context;
        private readonly ITokenService _tokenService;

        public AuthController(TDataContext context, ITokenService tokenService) 
        {
            _context = context;
            _tokenService = tokenService;
        }

        //User Register

        [HttpPost("register")] //POST: api/auth/register
        public async Task<ActionResult<UserDto>> UserRegister(RegisterDto registerDto) 
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
            var userToken = new UserDto
            {
                Username = user.UserName,
                Token = await _tokenService.CreateToken(user),
            };
            return Ok(userToken);
        }

        [HttpPost("login")] //POST: api/auth/register
        public async Task<ActionResult<UserDto>> UserLoging(LoginDto loginDto)
        {
           var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName.ToLower() ==  loginDto.Username.ToLower());
            if (user == null) return Unauthorized("Invalid Username");
            using var hamc = new HMACSHA512(user.PasswordSalt);
            var computedHash = hamc.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
            for(int i=0; i< computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid Passoword");
            }
            var userToken = new UserDto
            {
                Username = user.UserName,
                Token = await _tokenService.CreateToken(user),
            };

            return Ok(userToken);
        }

        private async Task<bool> IsUserExists(string username)
        {
            return await _context.Users.AnyAsync(x => x.UserName.ToLower() == username.ToLower());
        }
    }
}
