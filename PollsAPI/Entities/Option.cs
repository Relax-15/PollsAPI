using System.ComponentModel.DataAnnotations.Schema;

namespace PollsAPI.Entities;

[Table("Options")]
public class Option
{
    public int Id { get; set; }
    public string Text { get; set; }
    public List<Vote> Votes { get; set; }

    public int PollId { get; set; }
    public Poll Poll { get; set; }
}