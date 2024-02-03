using PollsAPI.Entities;

namespace PollsAPI.DTOs.Responses;

public class AuthResponse
{
    public string Token { get; set; }
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime? EmailVerifiedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}