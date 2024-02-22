using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using MauiBlazor.Shared.Models;
using MauiBlazor.Shared.Models.ResourceModels;
using Newtonsoft.Json;

namespace MauiBlazor.Mobile.Services;

public class UserService : IUserService
{


    public async Task<ResponseModel<AuthenticationResponse>> Signup(UserModel userModel)
    {
        var returnResponse = new ResponseModel<AuthenticationResponse>();

        try
        {
            var jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
            };

            var json = System.Text.Json.JsonSerializer.Serialize(userModel, jsonSerializerOptions);

            var httpClient = new HttpClient();
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await httpClient.PostAsync($"{App.BaseUrl}/Signup", stringContent);

            if (response != null && response.IsSuccessStatusCode)
            {
                returnResponse.Success = true;
                returnResponse.Message = "User registered successfully!";
            }
            else
            {
                returnResponse.Message = "Failed to register user.";
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error serializing object to JSON or sending request: {ex.Message}");
            returnResponse.Message = "An error occurred while registering the user.";
        }

        return returnResponse;

    }

    public async Task<ResponseModel<AuthenticationResponse>> Login(AuthenticationRequest request)
    {
        var returnResponse = new ResponseModel<AuthenticationResponse>();

        try
        {
            HttpClient httpClient = new();
            string apiUrl = $"{App.BaseUrl}/Users/Login";

            var jsonRequest = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PostAsync(apiUrl, httpContent);

            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                AuthenticationResponse authenticationResponse = JsonConvert.DeserializeObject<AuthenticationResponse>(result);

                if (authenticationResponse != null)
                {
                    returnResponse.Success = true;
                    returnResponse.Data = authenticationResponse;
                    returnResponse.Message = "Login Sucess";
                }
                else
                {
                    returnResponse.Message = "Credentials are incorrect";
                }
            }
            else
            {
                returnResponse.Message = "Login failed. Please try again.";
            }
        }
        catch (Exception ex)
        {
            returnResponse.Ex = ex;
        }

        return returnResponse;

    }
}
