using Blazored.Toast.Services;
using EnglishApiClient.Dtos.Entity;
using EnglishApiClient.HttpServices.Interfaces;
using Microsoft.AspNetCore.Components;

namespace EnglishApiClient.Pages.TypeOfTestingPage
{
    public partial class CreateTestingType
    {
        private TypeOfTesting _type = new TypeOfTesting();

        [Inject]
        private ITypeOfTestingHttpService _typeService { get; set; }

        [Inject]
        private IToastService _toastService { get; set; }

        [Inject]
        private NavigationManager _navigation { get; set; }

        private async void AddType()
        {
            var result = await _typeService.Create(_type);
            if (result)
            {
                _toastService.ShowSuccess($"Type with name {_type.Name} added successfully!");
                _navigation.NavigateTo("/testing-type");
            }
        }
    }
}
