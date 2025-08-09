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

        [HttpPut("update-user/{Id}")]
        public async Task<IActionResult> UpdateUser(long Id, [FromBody] User user)
        {
            var existingUser = await _repo.GetById(Id);
            if (existingUser == null)
            {
                return NotFound("User not found");
            }

            var existingUserObj = (User)existingUser;
            
            // Only update non-null properties
            if (!string.IsNullOrEmpty(user.Email))
                existingUserObj.Email = user.Email;
            if (!string.IsNullOrEmpty(user.Role))
                existingUserObj.Role = user.Role;
            
            // Don't allow updating hashed password directly for security
            // Password updates should be done through a separate endpoint

            var updatedUser = await _repo.UpdateById(Id, existingUserObj);
            if (updatedUser != null)
            {
                return Ok("User updated successfully");
            }
            else
            {
                return BadRequest("Failed to update user");
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> register([FromBody] RegisterDto register)
        {
            string message = await _authRepo.register(register);
          
           
                return Ok(new { message = "User is added" });
            




        }
        [HttpPost("login")]
        public async Task<IActionResult> login([FromBody] LoginDto login)
        {
            var token = await _authRepo.login(login);
            return Ok(new { token });
        }
    


    }

 

}