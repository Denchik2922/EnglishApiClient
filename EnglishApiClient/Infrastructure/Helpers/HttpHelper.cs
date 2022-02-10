using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;

namespace EnglishApiClient.Infrastructure.Helpers
{
    public static class HttpHelper
    {
        public static async Task<T> SendPostRequest<T>(string endpoint, Dictionary<string, string> bodyParams)
        {
            var httpContent = new FormUrlEncodedContent(bodyParams);
            return await SendHttpRequestAsync<T>(HttpMethod.Post, endpoint, httpContent: httpContent);
        }

        private static async Task<T> SendHttpRequestAsync<T>(HttpMethod httpMethod,
                                                             string endpoint,
                                                             Dictionary<string, string> queryParams = null,
                                                             HttpContent httpContent = null)
        {
            var url = queryParams != null
                ? QueryHelpers.AddQueryString(endpoint, queryParams)
                : endpoint;

            var request = new HttpRequestMessage(httpMethod, url);

            if (httpContent != null)
            {
                request.Content = httpContent;
            }

            using var httpClient = new HttpClient();
            using var response = await httpClient.SendAsync(request);

            var resultJson = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException(resultJson);
            }

            var result = JsonConvert.DeserializeObject<T>(resultJson);
            return result;
        }
    }
}
