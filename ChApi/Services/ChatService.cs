using System.Net.Http.Headers;
using ChApi.Models;

namespace ChApi.Services
{
    public class ChatService
    {
        private readonly HttpClient _httpClient;
        private const string ApiUrl = "https://api.thecatapi.com/v1/images/search"; // URL de l'API pour récupérer des images de chats
        private const string ApiKey = "live_oL2DAI60qh1z8PkZJlFjLkoT03XCg7M371oI976CfIUxyC9J1nEZ6StjeM0cm4mI"; // Clé API pour authentification

        /// <summary>
        /// Constructeur de ChatService qui initialise le HttpClient avec la clé API.
        /// </summary>
        /// <param name="httpClient">Instance de HttpClient injectée pour les appels API.</param>
        public ChatService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            if (!string.IsNullOrEmpty(ApiKey))
            {
                _httpClient.DefaultRequestHeaders.Add("x-api-key", ApiKey); // Ajout de la clé API aux en-têtes si elle est définie
            }
        }

        /// <summary>
        /// Récupère une image aléatoire de chat ayant une race associée depuis l'API.
        /// Gère les erreurs et renvoie null en cas d'échec de l'appel ou si aucun chat n'est disponible.
        /// </summary>
        /// <returns>Un objet Chat contenant l'image du chat aléatoire, ou null en cas d'erreur.</returns>
        public async Task<Chat?> GetRandomChatAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<Chat[]>($"{ApiUrl}?has_breeds=1");
                return response?.FirstOrDefault(); // Renvoie le premier élément ou null si la réponse est vide
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de l'appel à l'API : {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Récupère une liste d'images de chats associés à une race spécifique depuis l'API.
        /// Limite le nombre de résultats à 5.
        /// </summary>
        /// <param name="IdRace">ID de la race pour laquelle les images de chats doivent être récupérées.</param>
        /// <returns>Une liste d'objets Chat pour la race spécifiée ou une liste vide en cas d'erreur ou si la race n'est pas spécifiée.</returns>
        public async Task<List<Chat>> GetListeChatParRaceAsync(string IdRace)
        {
            if (string.IsNullOrEmpty(IdRace))
            {
                return new List<Chat>(); // Retourne une liste vide si l'ID de la race est null ou vide
            }

            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<Chat>>($"{ApiUrl}?breed_ids={IdRace}&limit=5");
                return response ?? new List<Chat>(); // Renvoie une liste vide si la réponse est null
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de l'appel à l'API : {ex.Message}");
                return new List<Chat>(); // Retourne une liste vide en cas d'exception
            }
        }
    }

}
