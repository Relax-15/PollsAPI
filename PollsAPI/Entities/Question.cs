using System.ComponentModel.DataAnnotations.Schema;

namespace PollsAPI.Entities;

[Table("Questions")]
public class Question
{
    public int Id { get; set; }
    public string Text { get; set; }
    public List<Option> Options { get; set; }
    
    public int PollId { get; set; }
    public Poll Poll { get; set; }
}