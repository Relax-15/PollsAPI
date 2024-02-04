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


    public VoteController(PollDbContext context)
    {
        _context = context;
    }

    [HttpPost("{userId}/{pollId}/AddVote")]
    public async Task<ActionResult<object>> AddVote(int userId, int pollId, VoteDto voteDto)
    {
        // check if poll is exist
        var poll = await _context.Polls.FindAsync(pollId);
        if (poll is null)
        {
            return NotFound("Poll not found.");
        }
        
        var vote = new Vote
        {
            UserId = userId,
            PollId = pollId,
            Answer = voteDto.Answer,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };
        _context.Votes.Add(vote);
        await _context.SaveChangesAsync();

        return vote;
    }
}