using System.ComponentModel.DataAnnotations;

namespace PollsAPI.DTOs;

public class PollDto
{
    [Required(ErrorMessage = "Title is required")]
    [MinLength(5, ErrorMessage = "Email is too short")]
    public string Title { get; set; }
    
    [JsonOptions(ErrorMessage = "Options must contain at least two elements and be in the specified format")]
    public string Options { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (!string.IsNullOrWhiteSpace(Options))
        {
            var options = Newtonsoft.Json.JsonConvert.DeserializeObject<string[]>(Options);
            if (options.Length < 2)
            {
                yield return new ValidationResult("Options must contain at least two elements",
                    new[] { nameof(Options) });
            }
        }
    }
}

public class JsonOptionsAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        try
        {
            if (value != null)
            {
                var options = Newtonsoft.Json.JsonConvert.DeserializeObject<string[]>(value.ToString());
                if (options.Length < 2)
                {
                    return new ValidationResult(ErrorMessage);
                }
            }
            return ValidationResult.Success;
        }
        catch (Exception)
        {
            return new ValidationResult(ErrorMessage);
        }
    }
}
