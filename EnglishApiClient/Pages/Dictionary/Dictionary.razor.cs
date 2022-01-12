using EnglishApiClient.Interfaces;
using Microsoft.AspNetCore.Components;
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnglishApiClient.Pages.Dictionary
{
    public partial class Dictionary
    {
        private ICollection<EnglishDictionary> englishDictionaries;

        [Inject]
        public IDictionaryHttpService DictionaryService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected async override Task OnInitializedAsync()
        {
            englishDictionaries = await DictionaryService.GetPublicDictionaries();
        }
        public void NavigateToDictionary(int id)
        {
            NavigationManager.NavigateTo($"dictionary/{id}");
        }
    }
}
