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
using Microsoft.AspNetCore.Authorization;

namespace BookStoreBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "admin")] // Only admins can access user management
    public class UserController : GenricController<User>
    {
        private readonly AuthRepo _authRepo;
        private readonly GenricRepoistry<User> _repo;

        public UserController(AuthRepo authRepo, GenricRepoistry<User> repo) : base(repo)
        {
            _authRepo = authRepo;
            _repo = repo;
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateUser(long Id, [FromBody] User user)
        {
            var existingUser = await _repo.GetById(Id);
            if (existingUser == null)
            {
                return NotFound("User not found");
            }

            var existingUserObj = (User)existingUser;
            
            
            if (!string.IsNullOrEmpty(user.Email))
                existingUserObj.Email = user.Email;
            if (!string.IsNullOrEmpty(user.Role))
                existingUserObj.Role = user.Role;
            
            

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

        [HttpPut("{Id}/role")]
        public async Task<IActionResult> UpdateUserRole(long Id, [FromBody] string role)
        {
            var existingUser = await _repo.GetById(Id);
            if (existingUser == null)
            {
                return NotFound("User not found");
            }

            var user = (User)existingUser;
            user.Role = role;

            var updatedUser = await _repo.UpdateById(Id, user);
            if (updatedUser != null)
            {
                return Ok("User role updated successfully");
            }
            else
            {
                return BadRequest("Failed to update user role");
            }
        }
    }
} 
