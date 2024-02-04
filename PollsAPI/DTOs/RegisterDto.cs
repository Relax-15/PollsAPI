using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PollsAPI.DTOs;

public class RegisterDto
{
    [Required(ErrorMessage = "Name is required")]
    [MinLength(2, ErrorMessage = "Name is too short")]
    public string Name { get; set; } 
    
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    [MinLength(5, ErrorMessage = "Email is too short")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Password is required")]
    [MinLength(5, ErrorMessage = "Password must be at least 5 characters long")]
    public string Password { get; set; }
}