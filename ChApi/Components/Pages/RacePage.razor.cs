using ChApi.Services;
using ChApi.Models;


namespace ChApi.Components.Pages
{
    public partial class RacePage
    {
        private List<Race> RaceChat = new(); // Liste des races de chats, initialisée pour éviter les erreurs de nullabilité
        private List<Chat> Chats = new(); // Liste des chats de la race sélectionnée, initialisée vide
        private string? selectedRaceId; // ID de la race sélectionnée, nullable car elle peut être non définie
        private bool isLoading = false; // Indique si une opération de chargement est en cours
        private string? errorMessage; // Message d'erreur à afficher en cas de problème, nullable

        /// <summary>
        /// Méthode appelée lors de l'initialisation de la page.
        /// Charge la liste des races de chats.
        /// </summary>
        protected override async Task OnInitializedAsync()
        {
            await LoadRaces();
        }

        /// <summary>
        /// Charge les races de chats depuis le service RaceService.
        /// Affiche un message d'erreur si aucune race n'est disponible ou en cas de problème de chargement.
        /// </summary>
        private async Task LoadRaces()
        {
            isLoading = true;
            errorMessage = null;
            try
            {
                RaceChat = await RaceService.GetListeRaceAsync() ?? new List<Race>();
                if (RaceChat.Count == 0)
                {
                    errorMessage = "Aucune race disponible.";
                }
            }
            catch (Exception ex)
            {
                errorMessage = $"Erreur lors du chargement des races : {ex.Message}";
            }
            finally
            {
                isLoading = false;
            }
        }

        /// <summary>
        /// Gère la sélection d'une race de chat.
        /// Définit l'ID de la race sélectionnée et charge les chats associés.
        /// </summary>
        /// <param name="RaceId">ID de la race sélectionnée.</param>
        private async Task OnRaceSelected(string RaceId)
        {
            selectedRaceId = RaceId;
            await ChargerChatParRace(RaceId);
        }

        /// <summary>
        /// Charge les chats correspondant à la race sélectionnée depuis le service ChatService.
        /// Affiche un message d'erreur si aucun chat n'est disponible pour cette race ou en cas d'erreur de chargement.
        /// </summary>
        /// <param name="RaceId">ID de la race sélectionnée pour filtrer les chats.</param>
        private async Task ChargerChatParRace(string RaceId)
        {
            isLoading = true;
            errorMessage = null;
            try
            {
                Chats = await ChatService.GetListeChatParRaceAsync(RaceId) ?? new List<Chat>();
                if (Chats.Count == 0)
                {
                    errorMessage = "Aucune image disponible pour cette race.";
                }
            }
            catch (Exception ex)
            {
                errorMessage = $"Erreur lors du chargement des images : {ex.Message}";
            }
            finally
            {
                isLoading = false;
            }
        }
    }
}
