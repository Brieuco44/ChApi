using System.Net.Http.Headers;
using ChApi.Models;

namespace ChApi.Services
{
    public class RaceService
    {
        private readonly HttpClient _httpClient;
        private const string ApiUrl = "https://api.thecatapi.com/v1/breeds"; // URL de l'API pour récupérer les races de chats
        private const string ApiKey = "live_oL2DAI60qh1z8PkZJlFjLkoT03XCg7M371oI976CfIUxyC9J1nEZ6StjeM0cm4mI"; // Clé API pour authentification

        /// <summary>
        /// Constructeur de RaceService qui initialise le HttpClient avec la clé API.
        /// </summary>
        /// <param name="httpClient">Instance de HttpClient injectée pour les appels API.</param>
        public RaceService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            if (!string.IsNullOrEmpty(ApiKey))
            {
                _httpClient.DefaultRequestHeaders.Add("x-api-key", ApiKey); // Ajout de la clé API aux en-têtes si elle est définie
            }
        }

        /// <summary>
        /// Récupère la liste des races de chats depuis l'API.
        /// Gère les erreurs en retournant une liste vide en cas d'échec de l'appel.
        /// </summary>
        /// <returns>Liste des races de chats ou une liste vide en cas d'erreur.</returns>
        public async Task<List<Race>> GetListeRaceAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<Race>>(ApiUrl);
                return response ?? new List<Race>(); // Retourne une liste vide si la réponse est null
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de l'appel à l'API : {ex.Message}");
                return new List<Race>(); // Retourne une liste vide en cas d'exception
            }
        }
    }

}
