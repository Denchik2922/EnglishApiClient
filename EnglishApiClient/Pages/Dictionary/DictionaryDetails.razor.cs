using EnglishApiClient.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Models;
using System.Threading.Tasks;

namespace EnglishApiClient.Pages.Dictionary
{
    public partial class DictionaryDetails
    {
        [Parameter]
        public int Id { get; set; }

        [Inject]
        public IJSRuntime _jsRuntime { get; set; }

        [Inject]
        public IDictionaryHttpService DictionaryService { get; set; }

        EnglishDictionary dictionary;

        protected override async Task OnInitializedAsync()
        {
            dictionary = await DictionaryService.GetById(Id);
        }

        public async Task PlaySound()
        {
            await _jsRuntime.InvokeAsync<string>("PlayAudio", "roar");
        }
    }
}
