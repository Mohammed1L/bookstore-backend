using BookstoreDB.Data;
using Microsoft.EntityFrameworkCore;
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

using BookstoreDB.Data;
using BookStoreBackend.Dtos;
using BookStoreBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace Repo
{
    public class BookRepoistry : GenricRepoistry<Order>
    {
        private readonly DbSet<Book> _dbSet;
        private readonly BookstoreContext db;

        public BookRepoistry(BookstoreContext db) : base(db)
        {
            _dbSet = db.Set<Book>();
            this.db = db;
        }

        public async Task<Book> postBook(BookDto book)
        {
            var exist = await db.Books.FirstOrDefaultAsync(b => b.Title == book.Title);
            if (exist != null)
            {
                exist.Inventory++;
                await db.SaveChangesAsync();
                return exist;
            }
            else
            {
                var bookDetails = new Book
                {
                    Title = book.Title,
                    Description = string.IsNullOrWhiteSpace(book.Description) ? "No description provided" : book.Description,
    ISBN = string.IsNullOrWhiteSpace(book.ISBN) ? "UNKNOWN-ISBN" : book.ISBN,
                    Price = book.Price,
                    Inventory = book.Inventory,
                    AuthorId = book.AuthorId,
                    CategoryId = book.CategoryId,
                    ImageUrl = book.ImageUrl,
                    CreatedBy = "none for now",
                };
                await db.Books.AddAsync(bookDetails);
                await db.SaveChangesAsync();
                return bookDetails;
            }
        }

        public async Task<List<BookDetailsDto>> GetBooks()
        {
            var books = await db.Books
                .Include(b => b.Author)
                .Include(b => b.Category)
                .Select(b => new BookDetailsDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    Description = b.Description,
                    Price = b.Price,
                    ImageUrl = b.ImageUrl,
                    AuthorName = b.Author.Name,
                    CategoryName = b.Category.Name,
                    AuthorId = (int)b.Author.Id
                })
                .ToListAsync();
            Console.WriteLine(books);
            return books;
        }
        public async Task<List<Book>> getBookByAuthor(int AuthorId)
        {
            var books = await db.Books.Where(b => b.AuthorId == AuthorId).ToListAsync();
            if (!books.Any())
            {
                throw new KeyNotFoundException("No books assigned to this author ");
            }

            Console.WriteLine(books);
            return books;
        }



        public async Task<List<Book>> getBookByOrder(int orderId)
        {
                        
            var books = await db.Books
                .Where(b => db.OrderItems
                    .Where(o => o.OrderId == orderId)
                    .Select(o => o.BookId)
                    .Contains(b.Id))
                .ToListAsync();
            
            if (!books.Any())
            {
                throw new KeyNotFoundException("No books assigned to this order ");
            }

            Console.WriteLine(books);
            return books;
        }

    }
}


