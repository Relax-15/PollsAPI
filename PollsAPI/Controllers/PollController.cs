using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PollsAPI.Data;
using PollsAPI.Entities;
using PollsAPI.Interfaces;

namespace PollsAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PollController: ControllerBase
{
    private readonly PollDbContext _context;
    private readonly ITokenService _tokenService;


    public PollController(IPollService service, ITokenService tokenService, PollDbContext context)
    {
        _context = context;
        _tokenService = tokenService;
    }

    [HttpPost("createPoll")]
    public async Task<ActionResult<Poll>> CreatePoll(Poll poll)
    {
        var newPoll = new Poll
        {
            Title = poll.Title,
            UserId = _tokenService.ExtractUserIdFromToken(Request.Headers["Authorization"]),
            Options = poll.Options,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.Polls.Add(poll);
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
    
}