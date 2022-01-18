using EnglishApiClient.Infrastructure;
using EnglishApiClient.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnglishApiClient.Pages.Dictionary
{
    public partial class Dictionary : IDisposable
    {
        private ICollection<EnglishDictionary> englishDictionaries;

        private ICollection<Tag> tags;

        [Inject]
        public ITagHttpService TagService { get; set; }
        [Inject]
        public IDictionaryHttpService DictionaryService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public HttpInterceptorService Interceptor { get; set; }
        protected async override Task OnInitializedAsync()
        {
            Interceptor.RegisterEvent();
            await GetDictionaries();
            await GetTags();
        }

        private async Task GetDictionaries()
        {
            englishDictionaries = await DictionaryService.GetPublicDictionaries();
        }

        private async Task GetTags()
        {
            tags = await TagService.GetAll();
        }
        public void NavigateToDictionary(int id)
        {
            NavigationManager.NavigateTo($"dictionary/{id}");
        }
        public void Dispose() => Interceptor.DisposeEvent();
    }
}
