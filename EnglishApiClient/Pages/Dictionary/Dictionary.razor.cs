using EnglishApiClient.Dtos.Entity;
using EnglishApiClient.HttpServices.Interfaces;
using EnglishApiClient.Infrastructure;
using Microsoft.AspNetCore.Components;

namespace EnglishApiClient.Pages.Dictionary
{
    public partial class Dictionary : IDisposable
    {
        private ICollection<EnglishDictionary> _englishDictionaries = new List<EnglishDictionary>();

        private ICollection<Tag> _tags = new List<Tag>();

        [Inject]
        private ITagHttpService _tagService { get; set; }

        [Inject]
        private IDictionaryHttpService _dictionaryService { get; set; }

        [Inject]
        private NavigationManager _navigation { get; set; }

        [Inject]
        private HttpInterceptorService _interceptor { get; set; }

        protected async override Task OnInitializedAsync()
        {
            _interceptor.RegisterEvent();
            await GetDictionaries();
            await GetTags();
        }

        private async Task GetDictionaries()
        {
            _englishDictionaries = await _dictionaryService.GetPublicDictionaries();
        }

        private async Task GetTags()
        {
            _tags = await _tagService.GetAll();
        }
        public void Dispose() => _interceptor.DisposeEvent();
    }
}
