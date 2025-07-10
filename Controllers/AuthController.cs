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
using Repo;
namespace BookStoreBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : GenricController<User>
    {


         private readonly AuthRepo _authRepo;
         private readonly GenricRepoistry<User> _repo;
            public AuthController(AuthRepo authRepo, GenricRepoistry<User> repo) : base(repo)
            {
                _authRepo= authRepo;
                  _repo = repo;
            }

        [HttpPost("register")]
        public async Task<IActionResult> register([FromBody] RegisterDto register)
        {
            string message = await _authRepo.register(register);
            Console.WriteLine(message);
            if (message == "User was not found")
            {
                return NotFound();

            }
            else if (message == "User information does not match")
            {
                return BadRequest("User information does not match");
            }
            else
            {
                return Ok(message);
            }




        }
        [HttpPost("login")]
        public async Task<IActionResult> login([FromBody] LoginDto login)
        {
            var token = await _authRepo.login(login);
            return Ok(new { token });
        }
    


    }

 

}