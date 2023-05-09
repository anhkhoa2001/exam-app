using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ExamApp.Models;
using Microsoft.IdentityModel.Tokens;

namespace ExamApp.Config;

public class JwtConfig
{
    public static readonly int exprire = 10;//minutes 
    public static string generationToken(string email, IConfiguration configuration)
    {
        var issuer = configuration["Jwt:Issuer"];
        var audience = configuration["Jwt:Audience"];
        var key = Encoding.ASCII.GetBytes
            (configuration["Jwt:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("Id", Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(JwtRegisteredClaimNames.Jti,
                    Guid.NewGuid().ToString())
            }),
            Expires = DateTime.UtcNow.AddMinutes(exprire),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials
            (new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwtToken = tokenHandler.WriteToken(token);

        return jwtToken;
    }

    public static bool verifyToken(string token, string uid, IConfiguration configuration)
    {
        return string.Equals(uid, GetUidFromToken(token, configuration));
    }
    
    public static string GetUidFromToken(string token, IConfiguration configuration)
    {
        try
        {
            var key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"]);
            var tokenHandler = new JwtSecurityTokenHandler();
            var claimsPrincipal = tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidateAudience = true,
                ValidAudience = configuration["Jwt:Audience"],
                ValidateLifetime = true
            }, out var validatedToken);

            var jwtToken = validatedToken as JwtSecurityToken;
            var uidClaim = jwtToken?.Claims.FirstOrDefault(c => c.Type == "email");

            if (uidClaim != null)
            {
                return uidClaim.Value;
            }
        }
        catch (SecurityTokenExpiredException e)
        {
            Console.WriteLine(e);
            return "Token is expired";
        }

        return null;
    }

}