using System;
namespace MauiBlazor.Shared.Models.ResourceModels;
public class AuthenticationResponse
{
    public string Token { get; set; } = string.Empty;
    public DateTime Expiration { get; set; }
    public string Username { get; set; } = string.Empty;

    public static implicit operator string(AuthenticationResponse v)
    {
        throw new NotImplementedException();
    }
}