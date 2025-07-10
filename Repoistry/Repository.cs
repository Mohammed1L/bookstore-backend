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

namespace BookRepo
{
    public class GenricRepoistry<T> where T : class // because ef core require this to be a class 
    {
        private readonly DbSet<T> _dbSet;
        private readonly BookstoreContext _context;// collection of genric entites tables 
        public GenricRepoistry(BookstoreContext db)
        {
            _dbSet = db.Set<T>();
            _context = db;

        }
        //                 A collection you can loop through (Enumerable)

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }
        public async Task<T> GetById(long Id)
        {
            return await _dbSet.FindAsync(Id);
        }

        public async Task<T> DeleteById(long Id)
        {
            var entity = await _dbSet.FindAsync(Id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();

                return entity;
            }


            else
            {
                return null;

            }
        }
        
       
    }
}