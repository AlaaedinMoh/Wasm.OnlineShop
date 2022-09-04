using System.Net.Http.Json;
using System.Net;

namespace OnlineShop.Wasm.Services
{
    public abstract class ServiceBase
    {
        protected readonly HttpClient httpClient;

        protected ServiceBase(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        protected virtual async Task<T> SendRequest<T>(string url)
        {
            HttpResponseMessage response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode.Equals(HttpStatusCode.NoContent))
                    return default(T);
                return await response.Content.ReadFromJsonAsync<T>();
            }
            var error = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException(error);
        }
    }
}
