using ChApi.Models;
using ChApi.Services;
using System.Text.Json;

namespace ChApi.Components.Pages
{
    public partial class Home
    {
        private Chat? chatImage;
        private List<Chat>? ListeFavoris;
        private bool isLoading = true;
        private string? errorMessage;
        private string jsonPath  = Path.Combine(Directory.GetCurrentDirectory(), "Favoris.json");

        protected override async Task OnInitializedAsync()
        {
            if (File.Exists(jsonPath))
            {
                var jsonData = File.ReadAllText(jsonPath);
                ListeFavoris = JsonSerializer.Deserialize<List<Chat>>(jsonData);
            }
            await NouveauChat();
        }

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

        // Ajouter un chat en favoris
        private async Task Ajouter(Chat ChatFavoris)
        {
            if (!ListeFavoris.Contains(ChatFavoris))
            {
                ListeFavoris.Add(ChatFavoris);
            }
            await Sauvegarder();
            await NouveauChat();
        }

        //Sauvegarde le JSONcdes favoris
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
