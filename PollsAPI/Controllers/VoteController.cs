using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PollsAPI.Data;
using PollsAPI.DTOs;
using PollsAPI.DTOs.Responses;
using PollsAPI.Entities;
using PollsAPI.Interfaces;

namespace PollsAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class VoteController : ControllerBase
{
    
    private readonly PollDbContext _context;
    private readonly ITokenService _tokenService;


    public VoteController(PollDbContext context, ITokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }

    [HttpPost]
    public async Task<ActionResult<object>> AddVote(PollDto pollDto)
    {
        // check if poll is exist
        var poll = await _context.Polls.FindAsync(pollDto.PollId);
        if (poll is null)
        {
            return NotFound("Poll not found.");
        }
        
        var userId = _tokenService.ExtractUserIdFromToken(Request.Headers["Authorization"]);
        var newVote = new Vote
        {
            UserId = userId,
            PollId = poll.Id,
            Answer = pollDto.Answer,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };
        _context.Votes.Add(newVote);
        await _context.SaveChangesAsync();

        return newVote;
    }
}