using EnglishApiClient.Infrastructure;
using EnglishApiClient.Interfaces;
using Microsoft.AspNetCore.Components;
using Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnglishApiClient.Pages.PrivateDictionary
{
    public partial class PrivateDictionaries
    {
        private ICollection<EnglishDictionary> englishDictionaries;

        [Inject]
        public IDictionaryHttpService DictionaryService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected async override Task OnInitializedAsync()
        {
            englishDictionaries = await DictionaryService.GetPrivateDictionaries();
        }
        public void NavigateToDictionary(int id)
        {
            NavigationManager.NavigateTo($"dictionary/{id}");
        }
    }
}
