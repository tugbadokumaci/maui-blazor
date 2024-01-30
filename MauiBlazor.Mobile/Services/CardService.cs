using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using MauiBlazor.Shared.Models;
using Newtonsoft.Json;

namespace MauiBlazor.Mobile.Data;

public class CardService : ContentPage
{


    public static async Task<ResponseModel<List<CardModel>>> GetHomePageDetails()
    {
        var returnResponse = new ResponseModel<List<CardModel>>();

        try
        {
            using (HttpClient client = new HttpClient())
            {
                string apiUrl = $"{App.BaseUrl}/Card/GetAllCards";

                // GET request
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();


                    returnResponse.Success = true;
                    returnResponse.Data = JsonConvert.DeserializeObject<List<CardModel>>(result);
                }
                returnResponse.Message = response.StatusCode.ToString(); // return info about response 
            }
        }
        catch (Exception ex)
        {
            returnResponse.Ex = ex;
        }
        return returnResponse;
    }

    public async Task<ResponseModel<string>> AddCard(CardModel cardModel)
    {

        string userId = "00883398-4f3c-48fa-83de-722e9305b0a9"; // sampleId

        var returnResponse = new ResponseModel<string>();

        try
        {
            var jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
            };

            var json = System.Text.Json.JsonSerializer.Serialize(cardModel, jsonSerializerOptions);

            var httpClient = new HttpClient();
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            var response = await httpClient.PostAsync($"{App.BaseUrl}/Card/createCardByUserId/{userId}", stringContent);

            if (response != null && response.IsSuccessStatusCode)
            {
                returnResponse.Success = true;
                returnResponse.Data = null;

            }

            returnResponse.Message = response.StatusCode.ToString(); // return info about response 
        }
        catch (Exception ex)
        {
            returnResponse.Ex = ex;
        }

        return returnResponse;

    }


    public async Task<ResponseModel<string>> DeleteCard(CardModel cardModel)
    {
        var returnResponse = new ResponseModel<string>();

        cardModel.IsActive = false;
        cardModel.UpdatedDate = DateTime.Parse(cardModel.CreatedDate.ToString());

        try
        {
            var jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Use CamelCase naming policy
                WriteIndented = true, // Optionally set this to true for indented JSON
            };
            var json = System.Text.Json.JsonSerializer.Serialize(cardModel, jsonSerializerOptions);

            var httpClient = new HttpClient();
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await httpClient.PutAsync($"{App.BaseUrl}/Card/updateCard/{cardModel.Id}", stringContent);

            if (response != null && response.IsSuccessStatusCode)
            {
                returnResponse.Success = true;
                returnResponse.Data = null;
            }

            returnResponse.Message = response.StatusCode.ToString(); // return info about response 
        }
        catch (Exception ex)
        {
            returnResponse.Ex = ex;
        }

        return returnResponse;
    }

    public async Task<ResponseModel<string>> UpdateCard(CardModel cardModel)
    {
        var returnResponse = new ResponseModel<string>();

        try
        {
            var jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Use CamelCase naming policy
                WriteIndented = true, // Optionally set this to true for indented JSON
            };
            var json = System.Text.Json.JsonSerializer.Serialize(cardModel, jsonSerializerOptions);

            var httpClient = new HttpClient();
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await httpClient.PutAsync($"{App.BaseUrl}/Card/updateCard/{cardModel.Id}", stringContent);

            if (response != null && response.IsSuccessStatusCode)
            {
                returnResponse.Success = true;
                returnResponse.Data = null;
            }

            returnResponse.Message = response.StatusCode.ToString(); // return info about response 
        }
        catch (Exception ex)
        {
            returnResponse.Ex = ex;
        }

        return returnResponse;
    }

    public static async Task<ResponseModel<CardModel>> GetCardDetailByCardId(int cardId)
    {
        var returnResponse = new ResponseModel<CardModel>();

        HttpClient httpClient = new HttpClient();

        string apiUrl = $"{App.BaseUrl}/Card/getCardDetails/{cardId}";

        // GET isteği oluştur
        HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

        if (response.IsSuccessStatusCode)
        {
            string result = await response.Content.ReadAsStringAsync();
            CardModel responseCardModel = JsonConvert.DeserializeObject<CardModel>(result);


            returnResponse.Success = true;
            returnResponse.Data = responseCardModel;

        }
        returnResponse.Message = response.StatusCode.ToString(); // return info about response 

        return returnResponse;
    }


    public static async Task<ResponseModel<List<CardModel>>> GetSavedCardsByUserId()
    {
        string userId = "00883398-4f3c-48fa-83de-722e9305b0a9"; // sampleId
        var returnResponse = new ResponseModel<List<CardModel>>();

        List<CardModel> Cards = new();


        try
        {
            using (HttpClient client = new HttpClient())
            {
                string apiUrl = $"{App.BaseUrl}/Card/GetSavedCards/{userId}";

                // GET request
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    List<SavedModel> SavedModelList = JsonConvert.DeserializeObject<List<SavedModel>>(result);
                    foreach (var savedModel in SavedModelList)
                    {
                        // Assuming there is another API endpoint to get CardModel details by Id
                        string cardApiUrl = $"{App.BaseUrl}/Card/GetCardDetails/{savedModel.CardId}";
                        HttpResponseMessage cardResponse = await client.GetAsync(cardApiUrl);

                        if (cardResponse.IsSuccessStatusCode)
                        {
                            string cardResult = await cardResponse.Content.ReadAsStringAsync();
                            CardModel cardModel = JsonConvert.DeserializeObject<CardModel>(cardResult);

                            // Add the cardModel to the ObservableCollection
                            Cards.Add(cardModel);
                        }
                        else
                        {
                            // Handle unsuccessful card response
                        }
                    }

                    returnResponse.Success = true;
                    returnResponse.Data = Cards;
                }
                returnResponse.Message = response.StatusCode.ToString(); // return info about response 
            }
        }
        catch (Exception ex)
        {
            returnResponse.Ex = ex;
        }
        return returnResponse;
    }

    public static async Task<ResponseModel<string>> SaveCardToUser(int cardId)
    {

        string userId = "00883398-4f3c-48fa-83de-722e9305b0a9"; // sampleId
        var returnResponse = new ResponseModel<string>();
         SavedModel savedModel = new SavedModel();
    
        savedModel.UserId = userId; 
        savedModel.CardId = cardId;

        try
        {
            var jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
            };

            var json = System.Text.Json.JsonSerializer.Serialize(savedModel, jsonSerializerOptions);

            var httpClient = new HttpClient();
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await httpClient.PostAsync($"{App.BaseUrl}/Card/SaveCard", stringContent);

            if (response != null && response.IsSuccessStatusCode)
            {
                returnResponse.Success = true;
            }

            returnResponse.Message = response.StatusCode.ToString(); // return info about response 
        }
        catch (Exception ex)
        {
            returnResponse.Ex = ex;
        }

        return returnResponse;
    }

}
