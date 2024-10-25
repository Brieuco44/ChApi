using ChApi.Models;
using ChApi.Services;
using System.Text.Json;

namespace ChApi.Components.Pages
{
    public partial class Home
    {
        private Chat? chatImage; // Stocke l'image de chat actuelle, peut être null
        private List<Chat> ListeFavoris = new(); // Liste des chats favoris, initialisée pour éviter null
        private bool isLoading = true; // Indique si l'application est en cours de chargement
        private string? errorMessage; // Message d'erreur à afficher en cas de problème, peut être null
        private readonly string jsonPath = Path.Combine(Directory.GetCurrentDirectory(), "Favoris.json"); // Chemin du fichier JSON pour stocker les favoris

        /// <summary>
        /// Méthode appelée lors de l'initialisation du composant.
        /// Charge les favoris depuis le fichier JSON si le fichier existe, puis charge un nouveau chat.
        /// </summary>
        protected override async Task OnInitializedAsync()
        {
            if (File.Exists(jsonPath))
            {
                var jsonData = File.ReadAllText(jsonPath);
                // Désérialise le JSON en liste de chats, gère le cas où jsonData est null ou incorrect
                ListeFavoris = JsonSerializer.Deserialize<List<Chat>>(jsonData) ?? new List<Chat>();
            }
            await NouveauChat();
        }

        /// <summary>
        /// Charge une nouvelle image de chat aléatoire en utilisant ChatService.
        /// Gère les erreurs en cas de problème lors du chargement de l'image.
        /// </summary>
        private async Task NouveauChat()
        {
            isLoading = true;
            errorMessage = null;
            try
            {
                chatImage = await ChatService.GetRandomChatAsync();
                if (chatImage == null)
                {
                    errorMessage = "Aucune image trouvée.";
                }
            }
            catch (Exception ex)
            {
                errorMessage = $"Erreur lors du chargement de l'image : {ex.Message}";
            }
            finally
            {
                isLoading = false;
            }
        }

        /// <summary>
        /// Ajoute un chat aux favoris si ce dernier n'est pas déjà présent dans la liste.
        /// Sauvegarde ensuite la liste mise à jour dans le fichier JSON.
        /// </summary>
        /// <param name="ChatFavoris">Le chat à ajouter aux favoris.</param>
        private async Task Ajouter(Chat ChatFavoris)
        {
            if (ListeFavoris != null && !ListeFavoris.Contains(ChatFavoris))
            {
                ListeFavoris.Add(ChatFavoris);
            }
            await Sauvegarder();
            await NouveauChat();
        }

        /// <summary>
        /// Sauvegarde la liste des chats favoris dans un fichier JSON.
        /// Gère les erreurs d'écriture dans le fichier et affiche un message d'erreur en cas de problème.
        /// </summary>
        private async Task Sauvegarder()
        {
            try
            {
                var json = JsonSerializer.Serialize(ListeFavoris, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(jsonPath, json);
            }
            catch (Exception ex)
            {
                errorMessage = $"Erreur lors de l'enregistrement des favoris : {ex.Message}";
            }
        }
    }

}
