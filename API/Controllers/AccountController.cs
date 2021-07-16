using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data;
using API.DTO;
using API.Entity;
using API.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseAPI
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;

        public AccountController(DataContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public string Register(RegisterDTO u){
            var user = new Auth{
                userName = u.userName,
                Password = u.Password
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return user.userName;
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(string UserName, string Password){
            
            var user = await _context.Users.SingleOrDefaultAsync(u => u.userName == UserName);
            
            if(user == null){
                return Unauthorized("Invalid Username");
            }

            if(user.Password != Password){
                return Unauthorized("Invalid Password");
            }
       
            return _tokenService.CreateToken(user);
        }


    }
}