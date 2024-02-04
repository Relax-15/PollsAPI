using System.ComponentModel.DataAnnotations;

namespace PollsAPI.DTOs;

public class VoteDto
{
    [Required]
    public string Answer { get; set; }
}