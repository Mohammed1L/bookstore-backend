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
namespace BookStoreBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase
{
    private readonly BookstoreContext db;
    public BookController(BookstoreContext _db)
    {
        db = _db;
    }
    [HttpGet]
    public async Task<IActionResult> getAllBooks() // Task = my method wait for async
    {

        var books = await db.Books.ToListAsync();

        return Ok(books);


    }
    [HttpPost]
    public async Task<IActionResult> postBook([FromBody] BookDto book)
    {


        var exist = await db.Books.FirstOrDefaultAsync(b => b.Title == book.Title);
        if (exist != null)
        { // Plus one in inventory
            Console.WriteLine("helllllllllo" + exist.Id);

            var modifyBook = await db.Books.FindAsync(exist.Id);
            modifyBook.Inventory++;
            await db.SaveChangesAsync();

            return Ok("Book is added to the inventory");


        }
        else
        {
            var bookDetails = new Book
            {
                Title = book.Title,
                Description = book.Description,
                ISBN = book.ISBN,
                Price = book.Price,
                Inventory = book.Inventory,
                AuthorId = book.AuthorId,
                CategoryId = book.CategoryId,
                CreatedBy = "none for now"

            };
            await db.Books.AddAsync(bookDetails);
            await db.SaveChangesAsync();
            return Ok("new book is added");

        }


    }
    [HttpGet("{Id}")]
    public async Task<IActionResult> getBook(long Id)
    {
        var book = await db.Books.FindAsync(Id);

        if (book != null)
        {
            return Ok(book);
        }
        else
        {
            return NotFound();
        }


    }
    [HttpDelete("{Id}")]
    public async Task<IActionResult> deletBook(long Id) {
         var book = await db.Books.FindAsync(Id);

        if (book != null)
        {
            db.Books.Remove(book);
            await db.SaveChangesAsync();
            return Ok(book);
        }
        else
        {
            return NotFound();
        }

     }
    

        
    }
    
    
    

