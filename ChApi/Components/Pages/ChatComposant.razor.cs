using ChApi.Models;
using ChApi.Services;
using Microsoft.AspNetCore.Components;

namespace ChApi.Components.Pages
{
    public partial class ChatComposant
    {
        [Parameter]
        public required Chat Chat { get; set; }

        [Parameter]
        public EventCallback<Chat> ChatChanged { get; set; }

        [Parameter]
        public required bool VoirRace { get; set; }

        [Parameter]
        public required bool VoirDescription { get; set; }

        [Parameter]
        public required bool VoirOrigine { get; set; }
    }
}
