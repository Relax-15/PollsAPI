using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PollsAPI.Data;
using PollsAPI.DTOs;
using PollsAPI.Entities;

namespace PollsAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PollController: ControllerBase
{
    private readonly PollDbContext _context;


    public PollController( PollDbContext context)
    {
        _context = context;
    }

    [HttpPost("{userId}/createPoll")]
    public async Task<ActionResult<Poll>> CreatePoll(int userId, PollDto poll)
    {
        var newPoll = new Poll
        {
            Title = poll.Title,
            UserId = userId,
            Options = poll.Options,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Polls.Add(newPoll);
        _context.SaveChanges();

        return newPoll;
    }
        
    [HttpGet("{pollId}/getPoll")]
    public async Task<ActionResult<Poll>> GetPoll(int id)
    {
        var poll =  _context.Polls.FirstOrDefault(p => p.Id == id);
        if (poll == null)
        {
            return NotFound("Poll not found");
        }
        
        return poll;
    }
    
    [HttpGet("getAllPolls")]
    public async Task<ActionResult<List<Poll>>> GetAllPolls()
    {
        return await _context.Polls.ToListAsync();
    }
}