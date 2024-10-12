using ChApi.Models;
using System.Linq;
using System.Text.Json;

namespace ChApi.Components.Pages
{
    public partial class FavorisPage
    {
        private Dictionary<string, List<Chat>> ListeAffichage;
        private List<Chat>? ListeFavoris = [];
        private string? errorMessage;
        private string jsonPath = Path.Combine(Directory.GetCurrentDirectory(), "Favoris.json");

        protected override async Task OnInitializedAsync()
        {
            AfficherChats();
        }
        private async void AfficherChats()
        {
            try
            {
                if (File.Exists(jsonPath))
                {
                    var jsonData = File.ReadAllText(jsonPath);
                    ListeFavoris = JsonSerializer.Deserialize<List<Chat>>(jsonData);
                    ListeAffichage = ListeFavoris
                        .Where(c => c.Race.Any()) // Assurez-vous qu'il y a des races définies
                        .GroupBy(c => c.Race.First().Nom) // Grouper par le nom de la race
                        .OrderBy(k => k.Key)
                        .ToDictionary(g => g.Key, g => g.ToList());
                }
            }
            catch (Exception ex)
            {
                errorMessage = $"Erreur lors du chargement des Chats : {ex.Message}";
            }
        }

        // Supprimer un chat des favoris
        private async Task Supprimer(Chat Chat)
        {
            if (ListeFavoris.Contains(Chat))
            {
                ListeFavoris.Remove(Chat);
                try
                {
                    var json = JsonSerializer.Serialize(ListeFavoris, new JsonSerializerOptions { WriteIndented = true });
                    File.WriteAllText(jsonPath, json);
                    AfficherChats();
                }
                catch (Exception ex)
                {
                    errorMessage = $"Erreur lors de l'enregistrement des favoris : {ex.Message}";
                }
            }
        }

        private void HandleChildButtonClick(int ChatId)
        {
            // Mettre à jour le message avec la valeur reçue depuis l'enfant
            //Message = $"Message depuis l'enfant : {messageFromChild}";
        }
    }
}
