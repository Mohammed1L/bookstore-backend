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
using Microsoft.AspNetCore.Authorization;

namespace BookStoreBackend.Controllers
// NOTES : i still didn't modify inventory based on quantity, and user is not connected to order yet and it needs more tests in the frontend 
{
    [ApiController]
    [Route("api/[controller]")]


    public class OrderController : GenricController<Order>
    {
        private readonly OrderRepo _orderRepo;
        private readonly GenricRepoistry<Order> _repo;
        public OrderController(OrderRepo orderRepo, GenricRepoistry<Order> repo) : base(repo)
        {
            _orderRepo = orderRepo;
            _repo = repo;
        }

        [Authorize]
        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateOrderStatus(long Id, [FromBody] string orderStatus)
        {
            var existingOrder = await _repo.GetById(Id);
            if (existingOrder == null)
            {
                return NotFound("Order not found");
            }

            // Validate order status
            var validStatuses = new[] { "Pending", "Processing", "Shipped", "Delivered", "Cancelled" };
            if (!validStatuses.Contains(orderStatus))
            {
                return BadRequest("Invalid order status. Valid statuses are: Pending, Processing, Shipped, Delivered, Cancelled");
            }

            var order = (Order)existingOrder;
            order.OrderStatus = orderStatus;

            var updatedOrder = await _repo.UpdateById(Id, order);
            if (updatedOrder != null)
            {
                return Ok("Order status updated successfully");
            }
            else
            {
                return BadRequest("Failed to update order status");
            }
        }

        [Authorize]
        [HttpPost]

        public async Task<IActionResult> postOrder([FromBody] OrderitemsDto order)
        {
            var orderDetail = await _orderRepo.postOrder(order);
            if (orderDetail != null)
            {
                return Ok(orderDetail.OrderItems);
            }
            else
            {
                return NotFound("O");
            }


        }
        
        [Authorize]
        [HttpGet("User")]
       
        public async Task<IActionResult> getOrder()
        {
            var orderDetail = await _orderRepo.GetOrder();
            if (orderDetail != null)
            {
                return Ok(orderDetail);
            }
            else
            {
                return NotFound("O");
            }


        }







    }
}