using System.Net.Http.Headers;
using ChApi.Models;

namespace ChApi.Services
{
    public class RaceService
    {
        private readonly HttpClient _httpClient;
        private const string ApiUrl = "https://api.thecatapi.com/v1/breeds";

        private const string ApiKey = "live_oL2DAI60qh1z8PkZJlFjLkoT03XCg7M371oI976CfIUxyC9J1nEZ6StjeM0cm4mI";

        public RaceService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            if (!string.IsNullOrEmpty(ApiKey))
            {
                _httpClient.DefaultRequestHeaders.Add("x-api-key", ApiKey);
            }
        }

        public async Task<List<Race>> GetListeRaceAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<Race>>(ApiUrl);
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de l'appel à l'API : {ex.Message}");
                return null;
            }
        }
    }
}
