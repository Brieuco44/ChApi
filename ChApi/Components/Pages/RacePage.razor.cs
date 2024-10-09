using ChApi.Services;
using ChApi.Models;


namespace ChApi.Components.Pages
{
    public partial class RacePage
    {
        private List<Race>? RaceChat = new();
        private List<Chat>? Chats = new();
        private string? selectedRaceId;
        private bool isLoading = false;
        private string? errorMessage;

        protected override async Task OnInitializedAsync()
        {
            await LoadRaces();
        }

        // Charger les races de chats
        private async Task LoadRaces()
        {
            isLoading = true;
            errorMessage = null;
            try
            {
                RaceChat = await RaceService.GetListeRaceAsync();
                if (RaceChat == null || RaceChat.Count == 0)
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

        // Quand une race est sélectionnée
        private async Task OnRaceSelected(string RaceId)
        {
            selectedRaceId = RaceId;
            await ChargerChatParRace(RaceId);
        }

        // Charger les chats en fonction de la race sélectionnée
        private async Task ChargerChatParRace(string RaceId)
        {
            isLoading = true;
            errorMessage = null;
            try
            {
                Chats = await ChatService.GetListeChatParRaceAsync(RaceId);
                if (Chats == null || Chats.Count == 0)
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
