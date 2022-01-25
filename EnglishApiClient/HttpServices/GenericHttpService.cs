using EnglishApiClient.HttpServices.Interfaces;
using System.Net.Http.Json;

namespace EnglishApiClient.HttpServices
{
        public abstract class GenericHttpService<T> : IGenericHttpService<T> where T : class
        {
            protected readonly HttpClient httpClient;
            protected readonly string requestString;
            public GenericHttpService(HttpClient client, string requestUri)
            {
                httpClient = client;
                requestString = requestUri;
            }

            public async Task<T> GetById(int id)
            {
                return await httpClient.GetFromJsonAsync<T>($"{requestString}/{id}");
            }

            public async Task<ICollection<T>> GetAll()
            {
                return await httpClient.GetFromJsonAsync<List<T>>(requestString);
            }

            public virtual async Task<bool> Create(T entity)
            {
                var response = await httpClient.PostAsJsonAsync(requestString, entity);
                return response.IsSuccessStatusCode;
            }

            public async Task<bool> Update( T entity)
            {
                var response = await httpClient.PutAsJsonAsync($"{requestString}", entity);
                return response.IsSuccessStatusCode;
            }

            public async Task<bool> Delete(int id)
            {
                var response = await httpClient.DeleteAsync($"{requestString}/?id={id}");
                return response.IsSuccessStatusCode;
            }
        }
}
