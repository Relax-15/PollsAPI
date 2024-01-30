using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PollsAPI.Data;
using PollsAPI.DTOs;
using PollsAPI.Entities;
using PollsAPI.Interfaces;

namespace PollsAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountAuthController: ControllerBase
{
    private readonly PollDbContext _context;
    private readonly ITokenService _tokenService;


    public AccountAuthController(PollDbContext context, ITokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<AuthDto>> Register(User user)
    {
        var userExists = _context.Users.FirstOrDefault(x => x.Username == user.Username.ToLower());
        if (userExists is not null) return BadRequest("Username is taken");
        
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        //return Ok();
        return new AuthDto()
        {
            Username = user.Username,
            Token = _tokenService.CreateToken(user)
        };
    }
    
    [HttpPost("login")]
    public async Task<ActionResult<AuthDto>> Login(User user)
    {
        var existingUser = _context.Users.FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password);
        if (existingUser == null)
            return NotFound("Username not found");

        //return Ok("Logged In");
        return new AuthDto()
        {
            Username = user.Username,
            Token = _tokenService.CreateToken(user)
        };
    }

}