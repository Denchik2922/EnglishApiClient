using EnglishApiClient.Infrastructure.RequestFeatures;
using System.Net.Http.Json;
using System.Text.Json;

namespace EnglishApiClient.Infrastructure.Helpers
{
    public static class CustomQueryHelper
    {
        public static Dictionary<string, string> GetQueryString(PaginationParameters parameters)
        {
            var tags = String.Join(",", parameters.SearchParameters.SearchTags);
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = parameters.PageNumber.ToString(),
                ["pageSize"] = parameters.PageSize.ToString(),
                ["searchTerm"] = parameters.SearchParameters.SearchTerm == null ? "" : parameters.SearchParameters.SearchTerm,
                ["searchTags"] = tags == null ? "" : tags,
                ["orderBy"] = parameters.OrderBy
            };

            return queryStringParam;
        }

        public static async Task<PagingResponse<T>> GetPaginationResponse<T>(HttpResponseMessage response) where T : class
        {
            return new PagingResponse<T>
            {
                Items = await response.Content.ReadFromJsonAsync<List<T>>(),
                MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First())
            };
        }
    }
}
