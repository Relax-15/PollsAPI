using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PollsAPI.Entities;

[Table("Votes")]
public class Vote
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Answer { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    
    public int PollId { get; set; }
    public Poll Poll { get; set; }

}