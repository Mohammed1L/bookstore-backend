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
using Microsoft.AspNetCore.Authorization;


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
    [Authorize(Roles = "Admin")] 
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

    [HttpPut("{Id}")]
        [Authorize(Roles = "Admin")] // Only admins can edit books

    public async Task<IActionResult> UpdateBook(long Id, [FromBody] BookDto bookDto)
    {
        var existingBook = await _repo.GetById(Id);
        if (existingBook == null)
        {
            return NotFound("Book not found");
        }

        var book = (Book)existingBook;


        if (!string.IsNullOrEmpty(bookDto.Title))
            book.Title = bookDto.Title;
        if (!string.IsNullOrEmpty(bookDto.Description))
            book.Description = bookDto.Description;
        if (!string.IsNullOrEmpty(bookDto.ISBN))
            book.ISBN = bookDto.ISBN;
        if (bookDto.Price > 0)
            book.Price = bookDto.Price;
        if (bookDto.Inventory >= 0)
            book.Inventory = bookDto.Inventory;
        if (bookDto.AuthorId > 0)
            book.AuthorId = bookDto.AuthorId;
        if (bookDto.CategoryId > 0)
            book.CategoryId = bookDto.CategoryId;
        if (!string.IsNullOrEmpty(bookDto.ImageUrl))
            book.ImageUrl = bookDto.ImageUrl;

        var updatedBook = await _repo.UpdateById(Id, book);
        if (updatedBook != null)
        {
            return Ok("Book updated successfully");
        }
        else
        {
            return BadRequest("Failed to update book");
        }
    }

    [HttpGet]
    public override async Task<IActionResult> GetAll()
    {
        var items = await _bookRepo.GetBooks();
        return Ok(items);
    }


    [HttpGet("by-author/{AuthorId}")]
    public async Task<IActionResult> getBookByAuthor(int AuthorId)
    {
        var items = await _bookRepo.getBookByAuthor(AuthorId);
        return Ok(items);
    }

    [HttpGet("by-order/{orderId}")]
    public async Task<IActionResult> getBookByOrder(int orderId)
    {
        var items = await _bookRepo.getBookByOrder(orderId);
        return Ok(items);
    }










}
    
    
    

