using EnglishApiClient.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using Models;
using Models.Dictionary;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EnglishApiClient.Pages.Dictionary
{
    public partial class DictionaryAdd
    {
        public DictionaryAddModel _dictionary = new DictionaryAddModel();
        private ICollection<Tag> tags;
        public int[] SelectedTags { get; set; } = new int[] {};

        [Inject]
        public IDictionaryHttpService DictionaryService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public ITagHttpService TagService { get; set; }

        [Inject]
        public AuthenticationStateProvider authProvider { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await GetTags();
        }

        private async Task GetTags()
        {
            tags = await TagService.GetAll();
        }

        private async void AddDictionary()
        {
            var authState = await authProvider.GetAuthenticationStateAsync();
            var userId = authState.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            
            _dictionary.UserId= userId;
            _dictionary.Tags = SelectedTags.Select(t => new Tag() { Id = t }).ToList();
            var result = await DictionaryService.Create(_dictionary);
            if (result)
            {
                NavigationManager.NavigateTo("/");
            }
        }
    }
}
