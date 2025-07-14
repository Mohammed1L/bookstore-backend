using BookstoreDB.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using BookStoreBackend.Models;
using BookStoreBackend.Dtos;
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
using services;

namespace Repo
{
    public class AuthRepo : GenricRepoistry<User>
    {
        private readonly DbSet<User> _dbSet;
        private readonly BookstoreContext _db;
        private readonly TokenServices _token;

        public AuthRepo(BookstoreContext db, TokenServices token) : base(db) // Base(db) pass db to parent class 
        {
            _dbSet = db.Set<User>();
            _db = db;
            _token = token;

        }

        public async Task<string> login(LoginDto login)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == login.Email);
            if (user == null)
            {
                throw new UnauthorizedAccessException("Password or user are wrong.");
                
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
                    var token = _token.GenerateToken(login.Email, login.Role);
                    return token;
                }
                else
                {
                    throw new UnauthorizedAccessException("Password or user are wrong."); // Handeled in the middleware
                
                }
            }
        }
        public async Task<string> register(RegisterDto register)
        {
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
                    return "user is added";
                }
                else
                {
                    throw new KeyNotFoundException("User can't be found"); 
                   

                }





            }
        }
    }
}
