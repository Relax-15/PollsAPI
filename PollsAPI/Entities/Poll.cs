

namespace PollsAPI.Entities;

public class Poll
{
    
    
    public int Id { get; set; }
    public int UserId { get; set; }
    public virtual User User { get; set; }
    public string Title { get; set; }
    public string Options { get; set; }
    public DateTime CreatedAt { get; set; }
    
    
}

