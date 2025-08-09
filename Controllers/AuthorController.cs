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
    public class AuthorController : GenricController<Author>
    {


        private readonly AuthRepo _authRepo;
        private readonly GenricRepoistry<Author> _repo;
        public AuthorController(AuthRepo authRepo, GenricRepoistry<Author> repo) : base(repo)
        {
            _authRepo = authRepo;
            _repo = repo;
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateAuthor(long Id, [FromBody] Author author)
        {
            var existingAuthor = await _repo.GetById(Id);
            if (existingAuthor == null)
            {
                return NotFound("Author not found");
            }

            var existingAuthorObj = (Author)existingAuthor;
            
            // Only update non-null properties
            if (!string.IsNullOrEmpty(author.Name))
                existingAuthorObj.Name = author.Name;
            if (!string.IsNullOrEmpty(author.Bio))
                existingAuthorObj.Bio = author.Bio;

            var updatedAuthor = await _repo.UpdateById(Id, existingAuthorObj);
            if (updatedAuthor != null)
            {
                return Ok("Author updated successfully");
            }
            else
            {
                return BadRequest("Failed to update author");
            }
        }
    }
}