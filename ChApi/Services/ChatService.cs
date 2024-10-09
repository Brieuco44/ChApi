using System.Net.Http.Headers;
using ChApi.Models;

namespace ChApi.Services
{
    public class ChatService
    {
        private readonly HttpClient _httpClient;
        private const string ApiUrl = "https://api.thecatapi.com/v1/images/search";

        private const string ApiKey = "live_oL2DAI60qh1z8PkZJlFjLkoT03XCg7M371oI976CfIUxyC9J1nEZ6StjeM0cm4mI";

        public ChatService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            if (!string.IsNullOrEmpty(ApiKey))
            {
                _httpClient.DefaultRequestHeaders.Add("x-api-key", ApiKey);
            }
        }

        public async Task<Chat>? GetRandomChatAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<Chat[]>($"{ApiUrl}?has_breeds=1");

                if (response != null)
                {
                    return response[0];
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de l'appel à l'API : {ex.Message}");
                return null;
            }
        }
            public async Task<List<Chat>>? GetListeChatParRaceAsync(string IdRace)
            {
                try
                {
                    if (string.IsNullOrEmpty(IdRace))
                    {
                        return null;
                    }
                    var response = await _httpClient.GetFromJsonAsync<List<Chat>>($"{ApiUrl}?breed_ids={IdRace}&limit=5");
                    if (response != null)
                    {
                        return response;
                    }
                    return null;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erreur lors de l'appel à l'API : {ex.Message}");
                    return null;
                }
            
        }
    }
}
