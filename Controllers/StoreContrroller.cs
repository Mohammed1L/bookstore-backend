using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
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
using Repo;
using Microsoft.AspNetCore.Authorization;

namespace BookStoreBackend.Controllers
{



    [ApiController]
    [Route("api/[controller]")]
    public class StoreController : ControllerBase
    {
        private readonly BookstoreContext _context;
        private readonly GeoService _geoService;

        public StoreController(BookstoreContext context, GeoService geoService)
        {
            _context = context;
            _geoService = geoService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddStore([FromBody] Store store)
        {
            var (lat, lng) = await _geoService.GeocodeAddressAsync(store.Address);
            store.Latitude = lat;
            store.Longitude = lng;

            _context.Stores.Add(store);
            await _context.SaveChangesAsync();
            return Ok(store);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStores()
        {
            return Ok(await _context.Stores.ToListAsync());
        }

        [HttpGet("nearest")]
        public async Task<IActionResult> GetNearestStore(double userLat, double userLng)
        {
            var stores = await _context.Stores.ToListAsync();
            var nearest = stores
                .OrderBy(s => GetDistance(userLat, userLng, s.Latitude, s.Longitude))
                .FirstOrDefault();

            return Ok(nearest);
        }

   
private double GetDistance(double lat1, double lon1, double lat2, double lon2)
{
    var R = 6371; // Radius of Earth in km
    var dLat = DegreesToRadians(lat2 - lat1);
    var dLon = DegreesToRadians(lon2 - lon1);
    var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
            Math.Cos(DegreesToRadians(lat1)) * Math.Cos(DegreesToRadians(lat2)) *
            Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
    var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
    return R * c;
}

private double DegreesToRadians(double deg) => deg * (Math.PI / 180);
        


        


        
    }
}