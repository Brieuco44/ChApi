using System.Net.Http.Headers;
using ChApi.Models;

namespace ChApi.Services
{
    public class ChatService
    {
        private readonly HttpClient _httpClient;
        private const string ApiUrl = "https://api.thecatapi.com/v1/images/search?has_breeds=1";

        private const string ApiKey = "live_oL2DAI60qh1z8PkZJlFjLkoT03XCg7M371oI976CfIUxyC9J1nEZ6StjeM0cm4mI";

        public ChatService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            if (!string.IsNullOrEmpty(ApiKey))
            {
                _httpClient.DefaultRequestHeaders.Add("x-api-key", ApiKey);
                //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ApiKey);
            }
        }

        public async Task<Chat> GetRandomChatAsync()
        {
            try
            {

                var response = await _httpClient.GetFromJsonAsync<Chat[]>(ApiUrl);

                if (response != null && response.Length > 0)
                {
                    return response[0];
                }
                return null;
            }
            catch (Exception ex)
            {
                // Vous pouvez logger l'exception ici si vous avez un système de logging
                Console.WriteLine($"Erreur lors de l'appel à l'API : {ex.Message}");
                return null;
            }
        }
    }
}
