using System;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Text;
using MauiBlazor.Shared.Models.ResourceModels;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace MauiBlazor.Api.Services;

public class JwtService
{
    private const int EXPIRATION_MONTHS = 1;

    private readonly IConfiguration _configuration;

    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public AuthenticationResponse CreateToken(IdentityUser user)
    {
        var expiration = DateTime.UtcNow.AddMonths(EXPIRATION_MONTHS);

        var token = CreateJwtToken(
            CreateClaims(user),
            CreateSigningCredentials(),
            expiration
        );

        var tokenHandler = new JwtSecurityTokenHandler();

        return new AuthenticationResponse
        {
            Token = tokenHandler.WriteToken(token),
            Expiration = expiration,
            Id = user.Id,
            Username = user.UserName
        };
    }

    private JwtSecurityToken CreateJwtToken(Claim[] claims, SigningCredentials credentials, DateTime expiration) =>
            new JwtSecurityToken(
                _configuration["JWT:Issuer"],
                _configuration["JWT:Audience"],
                claims,
                expires: expiration,
                signingCredentials: credentials
            );

    private Claim[] CreateClaims(IdentityUser user) =>
        new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName), // veya kullanıcıya özgü başka bir kimlik bilgisi
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email)
        };

    private SigningCredentials CreateSigningCredentials() =>
        new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"])),
            SecurityAlgorithms.HmacSha256
        );
}
