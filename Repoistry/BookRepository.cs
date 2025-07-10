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

namespace Repo
{
    public class BookRepoistry : GenricRepoistry<Order>// because ef core require this to be a class 
    {
        private readonly DbSet<Book> _dbSet;
        private readonly BookstoreContext db;

        public BookRepoistry(BookstoreContext db) : base(db) // Base(db) pass db to parent class 
        {
            _dbSet = db.Set<Book>();
            this.db = db;

        }


        public async Task<Book> postBook(BookDto book)
        {


            var exist = await db.Books.FirstOrDefaultAsync(b => b.Title == book.Title);
            if (exist != null)
            { // Plus one in inventory


                var modifyBook = await db.Books.FindAsync(exist.Id);
                modifyBook.Inventory++;
                await db.SaveChangesAsync();


                return modifyBook;


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

                return bookDetails;

            }



        }


    }
}
