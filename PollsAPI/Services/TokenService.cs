using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using PollsAPI.Entities;
using PollsAPI.Interfaces;

namespace PollsAPI.Services;

public class TokenService: ITokenService
{
    private readonly SymmetricSecurityKey _key;

    public TokenService(IConfiguration config)
    {
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
    }
    public string CreateToken(User user)
    {
        var claims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.NameId, user.Name),
            new Claim("UserId", user.Id.ToString()) 
            
        };

        var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddHours(1),
            SigningCredentials = credentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
    //
    // public int ExtractUserIdFromToken(string token)
    // {
    //     var tokenHandler = new JwtSecurityTokenHandler();
    //     var jwtToken = tokenHandler.ReadJwtToken(token);
    //
    //     var userIdClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "UserId");
    //         
    //     if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
    //     {
    //         throw new ArgumentException("Invalid JWT token or user ID not found.");
    //     }
    //
    //     return userId;
    // }
}