using System;
using MauiBlazor.Shared.Models.ResourceModels;
using MauiBlazor.Shared.Models;
namespace MauiBlazor.Mobile.Services;

public interface IUserService
{
    Task<ResponseModel<AuthenticationResponse>> Signup(UserModel userModel);
    Task<ResponseModel<AuthenticationResponse>> Login(AuthenticationRequest request);
}
