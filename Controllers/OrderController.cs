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







        [HttpPost]
        public async Task<IActionResult> postOrder([FromBody] OrderitemsDto order)
        {
            var orderDetail = await _orderRepo.postOrder(order);
            if (orderDetail != null)
            {
                return Ok(orderDetail);
            }
            else
            {
                return NotFound();
            }


        }







    }
}