using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PollsAPI.Data;
using PollsAPI.DTOs;
using PollsAPI.Entities;
using PollsAPI.Interfaces;
using System.Security.Cryptography;
using System.Text;

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
    public async Task<ActionResult<AuthDto>> Register(UserDto userDto)
    {
        var userExists = _context.Users.FirstOrDefault(x => x.Username == userDto.Username.ToLower());
        if (userExists is not null) return BadRequest("Username is taken");

        using var hmac = new HMACSHA512();

        var user = new User
        {
            Username = userDto.Username,
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDto.Password)),
            PasswordSalt = hmac.Key
        };

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
    public async Task<ActionResult<AuthDto>> Login(UserDto userDto)
    {
        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == userDto.Username);
        if (existingUser is null)
            return NotFound("Username not found");

        using var hmac = new HMACSHA512(existingUser.PasswordSalt);

        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDto.Password));

        for (int i = 0; i < computedHash.Length; i++)
        {
            if (computedHash[i] != existingUser.PasswordHash[i]) return Unauthorized("Invalid Password");
        }

        //return Ok("Logged In");
        return new AuthDto()
        {
            Username = existingUser.Username,
            Token = _tokenService.CreateToken(existingUser)
        };
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<string>> GetUserById(int id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user == null)
            return NotFound("Username not found");

        return user.Username;
    }

    [HttpGet("getAllUsers")]
    public async Task<ActionResult<List<string>>> GetAllUsers()
    {
        return await _context.Users.Select(un => un.Username).ToListAsync();
    }

}