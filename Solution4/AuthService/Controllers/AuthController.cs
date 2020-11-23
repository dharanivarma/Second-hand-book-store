using AuthService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserDbContext _context;
        private IConfiguration _config;
        public AuthController(UserDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpPost("User")]
        public User GetUser(User valuser)
        {
            var user = _context.Users.FirstOrDefault(c => c.User_Name == valuser.User_Name && c.Password == valuser.Password);

            if (user == null)
            {

                return null;
            }

            return user;
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] User login)
        {
            IActionResult response = Unauthorized();
            User user = GetUser(login);
            if (user == null)
            {
                return NotFound();
            }
            // User user1=_context.Users.FirstOrDefault(u=>u.Username==)
            // var user = AuthenticateUser(login);
            //var user = _context.Users.FirstOrDefaultAsync(c => c.Username == login.Username && c.Password == login.Password);

            else
            {
                //log4net.Info("Login credential matched");
                return Ok(new
                {
                    token = GenerateJSONWebToken(user)
                });
            }
        }
        private string GenerateJSONWebToken(User userInfo)
        {

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                null,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
