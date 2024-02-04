using PollsAPI.DTOs;

namespace PollsAPI.UnitTests;

[TestFixture]
public class RegisterAndLoginTests
{

    [TestCase(null)]   // Null name
    [TestCase("")]          // Empty nam
    [TestCase("A")]         // Less than characters
    [TestCase("123")]       // Contains digits

    public void RegisterAndLogin_ShouldFail_WhenNameIsShortOrInvalid(string name)
    {
        // Arrange
        var registerDto = new RegisterDto
        {
            Name = name,
            Email = "john@example.com",
            Password = "password123",
        };
        // Act
        bool nameValid = IsValidName(name);
        // Assert
        Assert.That(nameValid, Is.EqualTo(false));
    }
    
    [TestCase(null)]   // Null email
    [TestCase("")]          // Empty email
    [TestCase("A")]         // Less than 5 characters
    [TestCase("email")]     // no domain
    [TestCase("email.com")] // No @ Symbol

    public void RegisterAndLogin_ShouldFail_WhenEmailIsShortOrInvalid(string email)
    {
        // Arrange
        var registerDto = new RegisterDto
        {
            Name = "Relax",
            Email = email,
            Password = "password123",
        };
        // Act
        bool isValid = IsValidEmail(email);
        // Assert
        Assert.That(isValid, Is.EqualTo(false));
    }
    
    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    [TestCase("1234")] // Less than 5 characters
    public void RegisterAndLogin_ShouldFail_WhenPasswordIsInvalid(string password)
    {
        // Arrange
        var registerDto = new RegisterDto
        {
            Name = "Relax",
            Email = "john@example.com",
            Password = password,
        };

        // Act
        bool isValid = IsValidPassword(password);
        // Assert
        Assert.That(isValid, Is.EqualTo(false));
    }
    
    
    
    private bool IsValidName(string name)
    {
        return !string.IsNullOrEmpty(name) && !name.Any(char.IsDigit) && !(name.Length <2);
    }
    
    private bool IsValidPassword(string password)
    {
        return !string.IsNullOrEmpty(password) && !(password.Length <5);
    }
    private bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }
    
    
}