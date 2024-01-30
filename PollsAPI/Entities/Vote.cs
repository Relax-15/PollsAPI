using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PollsAPI.Entities;

[Table("Votes")]
public class Vote
{
    public int Id { get; set; }
    
    public int User_Id { get; set; }
    public User User { get; set; }
    
    public int Poll_Id { get; set; }
    public Poll Poll { get; set; }
    
    public int Option_Id { get; set; }
    public Option Option { get; set; }
}