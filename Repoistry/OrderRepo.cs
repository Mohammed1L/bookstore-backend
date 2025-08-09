using BookstoreDB.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using BookStoreBackend.Models;
using BookStoreBackend.Dtos;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using services;
namespace Repo
{
    public class OrderRepo : GenricRepoistry<Order>
    {
        private readonly DbSet<Order> _dbSet;
        private readonly BookstoreContext _db;
        private readonly TokenServices _token;
        public OrderRepo(BookstoreContext db, TokenServices token) : base(db) // Base(db) pass db to parent class 
        {
            _dbSet = db.Set<Order>();
            _db = db;
            _token = token;

        }



        [HttpPost]
        public async Task<Order> postOrder([FromBody] OrderitemsDto order)
        {
            string userEmail = _token.GetEmailFromToken();

            var userDetails = await _db.Users.FirstOrDefaultAsync(u => u.Email == userEmail);
            if (userDetails == null)
                throw new UnauthorizedAccessException("User not found or unauthorized.");
            var orderDetails = new Order
            {
                OrderStatus = "Pending",
                UserId = userDetails.Id, // this is for test but i need to get the user Id from the token
                OrderItems = order.orderItems,
                // As i understand ba navigation property order items will be added with orderId fk auto
                Date = DateTime.Now


            };
            await _db.Orders.AddAsync(orderDetails);
            await _db.SaveChangesAsync();
            return orderDetails;



        }

        [HttpGet]
        [Authorize]
        public async Task<List<Order>> GetOrder()
        {
            string userEmail = _token.GetEmailFromToken();

            var userDetails = await _db.Users.FirstOrDefaultAsync(u => u.Email == userEmail);
            if (userDetails == null)
                throw new KeyNotFoundException("Order can't be found");

            var userOrder = await _db.Orders.Where(o => o.UserId == userDetails.Id).ToListAsync();
            if (userOrder == null)
                throw new KeyNotFoundException("Order can't be found");

            return userOrder;
        }

       
    }
}
