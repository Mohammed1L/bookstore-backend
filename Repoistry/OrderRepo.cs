using BookstoreDB.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using BookStoreBackend.Models;
using BookStoreBackend.Dtos;

namespace Repo
{
    public class OrderRepo : GenricRepoistry<Order>
    {
        private readonly DbSet<Order> _dbSet;
        private readonly BookstoreContext _db;

        public OrderRepo(BookstoreContext db) : base(db) // Base(db) pass db to parent class 
        {
            _dbSet = db.Set<Order>();
            _db = db;

        }



        [HttpPost]
        public async Task<Order> postOrder([FromBody] OrderitemsDto order)
        {


            var orderDetails = new Order
            {
                OrderStatus = "PendingTest",
                UserId = 1, // this is for test but i need to get the user Id from the token
                OrderItems = order.orderItems,
                // As i understand ba navigation property order items will be added with orderId fk auto
                Date = DateTime.Now





            };
            await _db.Orders.AddAsync(orderDetails);
            await _db.SaveChangesAsync();
            return orderDetails;



        }
    }
}