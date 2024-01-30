using PollsAPI.Entities;

namespace PollsAPI.Interfaces;

public interface IPollService
{
    Poll CreatePoll(Poll poll);
    Poll GetPoll(int pollId);
    void Vote(int pollId, Vote vote);
}