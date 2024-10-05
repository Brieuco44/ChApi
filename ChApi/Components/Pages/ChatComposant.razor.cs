using ChApi.Models;
using ChApi.Services;

namespace ChApi.Components.Pages
{
    public partial class ChatComposant 
    {
        private Chat chatImage;
        private bool isLoading = true;
        private string? errorMessage;

        protected override async Task OnInitializedAsync()
        {
            await ChercherChat();
        }

        private async Task ChercherChat()
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

        private async Task NouveauChat()
        {
            await ChercherChat();
        }
    }
}
