using Blazored.LocalStorage;
using MauiBlazor.Shared.Models;
using Newtonsoft.Json;

namespace MauiBlazor.Mobile.Services
{
    public class LocalService
    {
        private readonly ILocalStorageService localStorage;

        public LocalService(ILocalStorageService localStorage)
        {
            this.localStorage = localStorage ?? throw new ArgumentNullException(nameof(localStorage));
        }

        public async Task<ResponseModel<string>> AddNewCardToLatestScanedCards (int newScannedCardId)
        {
            var returnResponse = new ResponseModel<string>();

            try
            {
                var latestScannedListJson = Preferences.Get("scanedCards", string.Empty);
                var latestScannedList = string.IsNullOrEmpty(latestScannedListJson)
                    ? new List<int>()
                    : JsonConvert.DeserializeObject<List<int>>(latestScannedListJson);

                latestScannedList = latestScannedList?.Take(10).ToList() ?? new List<int>();
                latestScannedList.Add(newScannedCardId);
                latestScannedList = latestScannedList.TakeLast(10).ToList();

                var updatedListJson = JsonConvert.SerializeObject(latestScannedList);

                Preferences.Set("scanedCards", updatedListJson);

                returnResponse.Success = true;
            }
            catch (Exception ex)
            {
                returnResponse.Ex = ex;
                returnResponse.Data = null;
            }

            return returnResponse;
        }

        public async Task<ResponseModel<List<int>>> GetLatestScanedCards()
        {
            var returnResponse = new ResponseModel<List<int>>();

            try
            {
                var latestScannedListJson = Preferences.Get("scanedCards", string.Empty);
                var latestScannedList = string.IsNullOrEmpty(latestScannedListJson)
                    ? new List<int>()
                    : JsonConvert.DeserializeObject<List<int>>(latestScannedListJson);

                returnResponse.Success = true;
                returnResponse.Data = latestScannedList;
            }
            catch (Exception ex)
            {
                returnResponse.Ex = ex;
            }

            return returnResponse;
        }
    }
}
