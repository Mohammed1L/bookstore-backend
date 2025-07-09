using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BookstoreDB.Data;
using BookStoreBackend.Models;   
using BookStoreBackend.Dtos;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
namespace BookStoreBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {


        private readonly IConfiguration _config;
        private readonly BookstoreContext _db;


        public AuthController(IConfiguration config, BookstoreContext db)
        {
            _config = config;
            _db = db;

        }

        private string GenerateToken(string userName, string role)
        {
            var claims = new[]
            {
                    new Claim(ClaimTypes.Name, userName),
                    new Claim(ClaimTypes.Role, role)
                };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var sign = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(double.Parse(_config["Jwt:DurationInMinutes"])),
                signingCredentials: sign
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        [HttpPost("register")]
        public async Task<IActionResult> register([FromBody] RegisterDto register)
        {
            var exist = await _db.Users.FirstOrDefaultAsync(u => u.Email == register.Email);
            if (exist == null)
            {
                var hasher = new PasswordHasher<object>();
                var email = register.Email;
                var password = register.Password;
                var hashedPassword = hasher.HashPassword(null, password);
                var user = new User
                {
                    Email = email,
                    HashedPassword = hashedPassword,
                    Role = "user"

                };
                await _db.Users.AddAsync(user);
                await _db.SaveChangesAsync();
                return Ok("User is added");
            }
            else
            {
                return BadRequest("User already exist, please try again or log in");

            }





        }
        [HttpPost("login")]
        public async Task<IActionResult> login([FromBody] LoginDto login)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == login.Email);
            if (user == null)
            {
                return NotFound("User does not exist, please try again");
            }
            else
            {
                var email = login.Email;
                var password = login.Password;
                login.Role = user.Role;
                var hasher = new PasswordHasher<object>();
                var result = hasher.VerifyHashedPassword(null, user.HashedPassword, password);
                if (result == PasswordVerificationResult.Success)
                {
                    var token = GenerateToken(login.Email, login.Role);
                    return Ok(new {token});
                }
                else
                {
                    return Unauthorized();
                }
            }
        }
    


    }

 

}