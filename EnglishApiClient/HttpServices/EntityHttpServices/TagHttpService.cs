using EnglishApiClient.Dtos.Entity;
using EnglishApiClient.HttpServices.Interfaces;
using System.Net.Http.Json;

namespace EnglishApiClient.HttpServices.EntityHttpServices
{
    public class TagHttpService : GenericHttpService<Tag>, ITagHttpService
    {
        public TagHttpService(HttpClient httpClient) : base(httpClient, "tag") { }

        public async Task<ICollection<Tag>> GetAllWithoutPage()
        {
            var response = await httpClient.GetAsync($"{requestString}/all");
            return await response.Content.ReadFromJsonAsync<ICollection<Tag>>();
        }
    }
}
