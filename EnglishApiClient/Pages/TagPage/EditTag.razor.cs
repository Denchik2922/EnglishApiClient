using Blazored.Toast.Services;
using EnglishApiClient.Dtos.Entity;
using EnglishApiClient.HttpServices.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace EnglishApiClient.Pages.TagPage
{
    public partial class EditTag
    {
        [Parameter]
        public int TagId { get; set; }

        private Tag _tag = new Tag();

        [Inject]
        private ITagHttpService _tagService { get; set; }

        [Inject]
        private IToastService _toastService { get; set; }

        [Inject]
        private IJSRuntime _jsRuntime { get; set; }

        [Inject]
        private NavigationManager _navigation { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GetTag();
        }

        private async Task GetTag()
        {
            _tag = await _tagService.GetById(TagId);
        }

        private async Task DeleteTag()
        {
            var confirmed = await _jsRuntime.InvokeAsync<bool>("confirm",
                                                                $"Are you sure you want to delete tag with name {_tag.Name}?");
            if (confirmed)
            {
                bool result = await _tagService.Delete(TagId);
                if (result)
                {
                    _toastService.ShowSuccess($"Tag with name {_tag.Name} deleted successfully!");
                    _navigation.NavigateTo("/");
                }
                else
                {
                    _toastService.ShowError("Tag deleted failed!");
                }
            }
        }

        private async void TagUpdate()
        {
            var result = await _tagService.Update(_tag);
            if (result)
            {
                _toastService.ShowSuccess($"Tag with name {_tag.Name} edited successfully!");
                _navigation.NavigateTo("/tags");
            }
        }
    }
}
