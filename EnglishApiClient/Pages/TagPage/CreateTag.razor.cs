using Blazored.Toast.Services;
using EnglishApiClient.Dtos.Entity;
using EnglishApiClient.HttpServices.Interfaces;
using Microsoft.AspNetCore.Components;

namespace EnglishApiClient.Pages.TagPage
{
    public partial class CreateTag
    {
        private Tag _tag = new Tag();

        [Inject]
        private ITagHttpService _tagService { get; set; }

        [Inject]
        private IToastService _toastService { get; set; }

        [Inject]
        private NavigationManager _navigation { get; set; }

        private async void AddTag()
        {
            var result = await _tagService.Create(_tag);
            if (result)
            {
                _toastService.ShowSuccess($"Tag with name {_tag.Name} added successfully!");
                _navigation.NavigateTo("/tags");
            }
        }
    }
}
