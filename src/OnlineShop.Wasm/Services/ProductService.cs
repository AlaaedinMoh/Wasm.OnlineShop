using OnlineShop.Shared.Dtos;
using OnlineShop.Wasm.Services.Contracts;
using System.Net;
using System.Net.Http.Json;

namespace OnlineShop.Wasm.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient httpClient;

        public ProductService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            try
            {
                return await SendRequest<IEnumerable<ProductDto>>("api/Product");
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<ProductDto?> GetProductItem(int id)
        {
            try
            {
                return await SendRequest<ProductDto>($"api/Product/{id}");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async Task<T> SendRequest<T>(string url)
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
