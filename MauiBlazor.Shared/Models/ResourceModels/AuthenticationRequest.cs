using System;
using System.ComponentModel.DataAnnotations;

namespace MauiBlazor.Shared.Models.ResourceModels;

public class AuthenticationRequest
{
    [Required]
    public string Email { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
}
