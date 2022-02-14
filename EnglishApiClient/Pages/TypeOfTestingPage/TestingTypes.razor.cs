using EnglishApiClient.Dtos.Entity;
using EnglishApiClient.HttpServices.Interfaces;
using Microsoft.AspNetCore.Components;

namespace EnglishApiClient.Pages.TypeOfTestingPage
{
    public partial class TestingTypes
    {
        private ICollection<TypeOfTesting> _types = new List<TypeOfTesting>();

        [Inject]
        private ITypeOfTestingHttpService _typeService { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await GetTypes();
        }
        private async Task GetTypes()
        {
            _types = await _typeService.GetAllWithoutPage();
        }
    }
}
