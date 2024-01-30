

namespace PollsAPI.Entities;

public class Poll
{
    public int Id { get; set; }
    public string Question { get; set; }
    
    public List<Option> Options { get; set; }
    public List<Vote> Votes { get; set; }
    
    public string User_Id { get; set; }
    public User User { get; set; }

}

