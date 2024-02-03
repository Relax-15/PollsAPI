using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PollsAPI.Data;
using PollsAPI.DTOs;
using PollsAPI.Entities;
using PollsAPI.Interfaces;
using System.Security.Cryptography;
using System.Text;
using PollsAPI.DTOs.Responses;

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
    public async Task<ActionResult> Register(RegisterDto registerDto)
    {
        var userExists = await _context.Users.FirstOrDefaultAsync(x => x.Email == registerDto.Email.ToLower());
        if (userExists is not null) return BadRequest("Email already registered");

        using var hmac = new HMACSHA512();

        var user = new User
        {
            Name = registerDto.Name,
            Email = registerDto.Email,
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
            PasswordSalt = hmac.Key,
            EmailVerifiedAt = null,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var response = new AuthResponse()
        {
            Token = _tokenService.CreateToken(user),
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            EmailVerifiedAt = user.EmailVerifiedAt,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt
        };
        //return Ok();
        // return new PollDto()
        // {
        //     Username = user.Username,
        //     Token = _tokenService.CreateToken(user)
        // };

        return Ok(response);
    }
    
    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Login(LoginDto loginDto)
    {
        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);
        if (existingUser is null)
            return NotFound("Email not found");

        using var hmac = new HMACSHA512(existingUser.PasswordSalt);

        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

        for (int i = 0; i < computedHash.Length; i++)
        {
            if (computedHash[i] != existingUser.PasswordHash[i]) return Unauthorized("Invalid Password");
        }

        //return Ok("Logged In");
        return new AuthResponse()
        {
            Token = _tokenService.CreateToken(existingUser),
            Id = existingUser.Id,
            Name = existingUser.Name,
            Email = existingUser.Email,
            EmailVerifiedAt = existingUser.EmailVerifiedAt,
            CreatedAt = existingUser.CreatedAt,
            UpdatedAt = existingUser.UpdatedAt
        };
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<object>> GetUserById(int id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user == null)
            return NotFound("User not found");

        return new {user.Id, user.Name, user.Email, user.EmailVerifiedAt, user.CreatedAt, user.UpdatedAt};
    }
    
    [HttpGet("getAllUsers")]
    public async Task<ActionResult<List<string>>> GetAllUsers()
    {
        return await _context.Users.Select(n => n.Name).ToListAsync();
    }

}