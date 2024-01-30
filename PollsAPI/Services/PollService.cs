using Microsoft.EntityFrameworkCore;
using PollsAPI.Data;
using PollsAPI.Entities;
using PollsAPI.Interfaces;

namespace PollsAPI.Services;

public class PollService: IPollService
{
    private readonly PollDbContext _context;

    public PollService(PollDbContext context)
    {
        _context = context;
    }

    public Poll CreatePoll(Poll poll)
    {
        var newPoll = new Poll
        {
            Question = poll.Question,
            Options = poll.Options.Select(option => new Option
            {
                Text = option.Text
            }).ToList()
        };

        _context.Polls.Add(poll);
        _context.SaveChanges();

        return poll;
    }

    public Poll GetPoll(int pollId)
    {
        return _context.Polls.Include(p => p.Options).FirstOrDefault(p => p.Id == pollId);
    }

    public void Vote(int pollId, Vote vote)
    {
        var poll = _context.Polls.Include(p => p.Options).FirstOrDefault(p => p.Id == pollId);
        if (poll == null)
        {
            throw new InvalidOperationException("Poll not found");
        }

        // var option = poll.Options.FirstOrDefault(o => o.Id == vote.Option_Id);
        // if (option == null)
        // {
        //     throw new InvalidOperationException("Option not found");
        // }

        // Assuming each user can vote only once
        // var existingVote = _context.Votes.FirstOrDefault(v => v.PollId == pollId && v.UserId == voteDto.UserId);
        // if (existingVote != null)
        // {
        //     throw new InvalidOperationException("User has already voted on this poll");
        // }

        var newVote = new Vote
        {
            User_Id = vote.User_Id,
            Poll_Id = pollId,
            Option_Id = vote.Option_Id
        };

        _context.Votes.Add(vote);
        _context.SaveChanges();
    }
}