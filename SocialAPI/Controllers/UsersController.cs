using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialAPI.TData;
using SocialAPI.TEntities;

namespace SocialAPI.Controllers
{
    
    public class UsersController : BaseController 
    {
        private readonly TDataContext _context;
        public UsersController(TDataContext context)
        {
            _context = context;
        }

        //[HttpGet]
        //public ActionResult<IEnumerable<AppUser>> GetUser()
        //{
        //    var users = _context.Users.ToList();
        //    return Ok(users);
        //}

     
        [HttpGet]
        public async Task< ActionResult<IEnumerable<AppUser>>> GetUser()
        {
            var users = await _context.Users.ToListAsync();
            return Ok(users);
        }

        //[HttpGet("id")]
        //public ActionResult<AppUser> GetUserById(int id)
        //{
        //    var user = _context.Users.Find(id);
        //    return Ok(user);
        //}


        [HttpGet("get-userByid")]
        public async Task<ActionResult<AppUser>> GetUserById(int id)
        {
            var user = await _context.Users.FindAsync(id);
            return Ok(user);
        }
    }
}
