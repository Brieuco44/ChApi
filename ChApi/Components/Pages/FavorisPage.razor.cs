using ChApi.Models;
using System.Linq;
using System.Text.Json;

namespace ChApi.Components.Pages
{
    public partial class FavorisPage
    {
        private Dictionary<string, List<Chat>> ListeAffichage = new(); // Dictionnaire pour afficher les chats favoris, initialisé pour éviter null
        private List<Chat> ListeFavoris = new(); // Liste des favoris initialisée vide pour éviter les erreurs de nullabilité
        private string? errorMessage; // Message d'erreur à afficher en cas de problème, peut être null
        private readonly string jsonPath = Path.Combine(Directory.GetCurrentDirectory(), "Favoris.json"); // Chemin du fichier JSON pour stocker les favoris

        /// <summary>
        /// Méthode appelée lors de l'initialisation de la page.
        /// Charge et affiche les chats favoris.
        /// </summary>
        protected override async Task OnInitializedAsync()
        {
            AfficherChats();
        }

        /// <summary>
        /// Charge la liste des chats favoris depuis le fichier JSON et les regroupe par race pour l'affichage.
        /// Gère les erreurs si le fichier est introuvable ou si une exception survient lors de la lecture.
        /// </summary>
        private void AfficherChats()
        {
            try
            {
                if (File.Exists(jsonPath))
                {
                    var jsonData = File.ReadAllText(jsonPath);
                    // Désérialise en liste de chats, utilise une liste vide par défaut en cas d'échec de la désérialisation
                    ListeFavoris = JsonSerializer.Deserialize<List<Chat>>(jsonData) ?? new List<Chat>();
                    // Groupe les chats par nom de race et trie par ordre alphabétique
                    ListeAffichage = ListeFavoris
                        .Where(c => c.Race != null && c.Race.Any())
                        .GroupBy(c => c.Race.First().Nom)
                        .OrderBy(k => k.Key)
                        .ToDictionary(g => g.Key, g => g.ToList());
                }
            }
            catch (Exception ex)
            {
                errorMessage = $"Erreur lors du chargement des Chats : {ex.Message}";
            }
        }

        /// <summary>
        /// Supprime un chat des favoris, met à jour le fichier JSON et réactualise l'affichage.
        /// Gère les erreurs potentielles lors de la suppression et de l'enregistrement.
        /// </summary>
        /// <param name="Chat">Le chat à supprimer de la liste des favoris.</param>
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
    }
}
