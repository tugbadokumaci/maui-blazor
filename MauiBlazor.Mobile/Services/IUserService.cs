using System;
using MauiBlazor.Shared.Models.ResourceModels;
using MauiBlazor.Shared.Models;
namespace MauiBlazor.Mobile.Services;

public interface IUserService
{
    Task<AuthenticationResponse> Signup(UserModel userModel);
    Task<AuthenticationResponse> Login(AuthenticationRequest request);
}
