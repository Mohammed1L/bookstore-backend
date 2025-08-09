

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
using Repo;
namespace BookStoreBackend.Controllers;
using BookStoreBackend.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/[controller]")]

public class GenricController<T> : ControllerBase where T : class
{

    private readonly GenricRepoistry<T> _db;
    
    public GenricController(GenricRepoistry<T> db)
    {
        _db = db;
        
    }


    [HttpGet]
    public virtual async Task<IActionResult> GetAll()
    {
        var items = await _db.GetAll();
        return Ok(items);
    }

    [HttpGet("{Id}")]
    public async Task<IActionResult> getById(long Id)
    {
        var item = await _db.GetById(Id);
        
        return Ok(item);
    }

  
    [Authorize(Roles = "Admin")] // Only admins can edit books

    [HttpDelete("{Id}")]
    public async Task<IActionResult> delete(long Id)
    {
        var item = await _db.DeleteById(Id);
        
        return Ok(item + "is deleted");


        
    }




}