using System;
namespace MauiBlazor.Shared.Models.ResourceModels;
public class AuthenticationResponse
{
    public string Token { get; set; } = string.Empty;
    public DateTime Expiration { get; set; }
    public string Id { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
}