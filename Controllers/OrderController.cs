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
namespace BookStoreBackend.Controllers
// NOTES : i still didn't modify inventory based on quantity, and user is not connected to order yet and it needs more tests in the frontend 
{
    [ApiController]
    [Route("api/[controller]")]


    public class OrderController : ControllerBase
    {
        private readonly BookstoreContext _db;
        public OrderController(BookstoreContext db)
        {
            _db = db;
        }



        [HttpGet]
        public async Task<IActionResult> getOrders()
        {
            var orders = await _db.Orders.ToListAsync();
            return Ok(orders);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> getOrder(long Id)
        {
            var order = await _db.Orders.FindAsync(Id);
            return Ok(order);
        }
        [HttpPost]
        public async Task<IActionResult> postOrder([FromBody] OrderitemsDto order)
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
            return Ok(order + " is added");



        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> deleteOrder(long Id)
        {
            var order = await _db.Orders.FindAsync(Id);
            if (order != null)
            {
                _db.Orders.Remove(order);
                await _db.SaveChangesAsync();
                return Ok("Order is deleted");
            }
            else
            {
                return NotFound("Order does not exist");
            }

        }
    }
    
    
}