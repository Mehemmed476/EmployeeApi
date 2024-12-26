using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EmployeeApp.BL.DTOs.AppUserDTOs;
using EmployeeApp.BL.ExternalServices.Abstractions;
using EmployeeApp.Core.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace EmployeeApp.BL.ExternalServices.Implementations;

public class JwtTokenService : IJwtTokenService
{
    private readonly IConfiguration _configuration;
    public JwtTokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public string GenerateJwtToken(AppUser user)
    {
        List<Claim> claims = new List<Claim>()
        {
            new Claim("FirstName", user.FirstName),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.NameIdentifier,user.Id)
        };
        SymmetricSecurityKey secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
        SigningCredentials signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        
        JwtSecurityToken securityToken = new JwtSecurityToken
        (
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            signingCredentials: signingCredentials,
            claims: claims,
            expires: DateTime.Now.AddHours(1)
        );
        
        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}