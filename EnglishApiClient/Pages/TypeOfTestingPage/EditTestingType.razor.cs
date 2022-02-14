using Blazored.Toast.Services;
using EnglishApiClient.Dtos.Entity;
using EnglishApiClient.HttpServices.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace EnglishApiClient.Pages.TypeOfTestingPage
{
    public partial class EditTestingType
    {
        [Parameter]
        public int TypeId { get; set; }

        private TypeOfTesting _type = new TypeOfTesting();

        [Inject]
        private ITypeOfTestingHttpService _typeService { get; set; }

        [Inject]
        private IToastService _toastService { get; set; }

        [Inject]
        private IJSRuntime _jsRuntime { get; set; }

        [Inject]
        private NavigationManager _navigation { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GetType();
        }

        private async Task GetType()
        {
            _type = await _typeService.GetById(TypeId);
        }

        private async Task DeleteType()
        {
            var confirmed = await _jsRuntime.InvokeAsync<bool>("confirm",
                                                                $"Are you sure you want to delete type with name {_type.Name}?");
            if (confirmed)
            {
                bool result = await _typeService.Delete(TypeId);
                if (result)
                {
                    _toastService.ShowSuccess($"Type with name {_type.Name} deleted successfully!");
                    _navigation.NavigateTo("/testing-type");
                }
                else
                {
                    _toastService.ShowError("Type deleted failed!");
                }
            }
        }

        private async void TypeUpdate()
        {
            var result = await _typeService.Update(_type);
            if (result)
            {
                _toastService.ShowSuccess($"Type with name {_type.Name} edited successfully!");
                _navigation.NavigateTo("/testing-type");
            }
        }
    }
}
