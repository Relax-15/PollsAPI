using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PollsAPI.Data;
using PollsAPI.Entities;
using PollsAPI.Interfaces;

namespace PollsAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PollController: ControllerBase
{
    private readonly IPollService _service;

    public PollController(IPollService service)
    {
        _service = service;
    }

    [HttpPost("createPoll")]
        public async Task<ActionResult<Poll>> CreatePoll(Poll poll)
        {
            var newPoll = _service.CreatePoll(poll);
            return CreatedAtAction(nameof(GetPoll), new { id = poll.Id }, poll);
        }
        
    [HttpGet("{pollId}/getPoll")]
    public async Task<ActionResult<Poll>> GetPoll(int id)
    {
        var poll = _service.GetPoll(id);
        if (poll == null)
        {
            return NotFound();
        }
        return poll;
    }
    
    [HttpPost("{pollId}/vote")]
    public IActionResult VoteOnPoll(int pollId, Vote vote)
    {
        _service.Vote(pollId, vote);
        return NoContent();
    }
    
    // [HttpGet("{id}/results")]
    // public ActionResult<PollResultsDto> GetPollResults(int id)
    // {
    //     var poll = _service.GetPoll(id);
    //     if (poll == null)
    //     {
    //         return NotFound();
    //     }
    //
    //     var pollResults = new Poll
    //     {
    //         Question = poll.Question,
    //         Options = new List<Option>()
    //     };
    //
    //     foreach (var option in poll.Options)
    //     {
    //         var optionResult = new Option
    //         {
    //             Text = option.Text,
    //         };
    //         pollResults.Options.Add(optionResult);
    //     }
    //
    // }
}