using EnglishApiClient.Dtos.Entity;
using EnglishApiClient.HttpServices.Interfaces;
using System.Net.Http.Json;

namespace EnglishApiClient.HttpServices.EntityHttpService
{
    public class TypeOfTestingHttpService : GenericHttpService<TypeOfTesting>, ITypeOfTestingHttpService
    {
        public TypeOfTestingHttpService(HttpClient httpClient) : base(httpClient, "TypeOfTesting") { }

        public async Task<ICollection<TypeOfTesting>> GetAllWithoutPage()
        {
            var response = await httpClient.GetAsync($"{requestString}");
            return await response.Content.ReadFromJsonAsync<ICollection<TypeOfTesting>>();
        }
    }
}
