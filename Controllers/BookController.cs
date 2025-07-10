using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BookstoreDB.Data;
using BookStoreBackend.Models;   
using BookStoreBackend.Dtos;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Xml.Schema;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.VisualBasic;
using BookStoreBackend.Controllers;
using Repo;


namespace BookStoreBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookController : GenricController<Book>
{
    private readonly BookRepoistry _bookRepo;
    private readonly GenricRepoistry<Book> _repo;
    public BookController(BookRepoistry bookRepo, GenricRepoistry<Book> repo) : base(repo)
    {
        _bookRepo = bookRepo;
        _repo = repo;
    }


    [HttpPost]
    public async Task<IActionResult> postBook([FromBody] BookDto book)
    {
        var bookDetail = await _bookRepo.postBook(book);
        if (bookDetail != null)
        {
            return Ok("Book is added");
        }
        else
        {
            return NotFound();
        }
        


      


    }
   

        
    }
    
    
    

