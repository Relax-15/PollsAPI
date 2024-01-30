namespace PollsAPI.Entities;


public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public List<Poll> Polls { get; set; }
    public List<Vote> Votes { get; set; }
}