using OnlineShop.Shared.Dtos;
using OnlineShop.Wasm.Services.Contracts;
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

        public Task<IEnumerable<ProductDto>> GetProducts()
        {
			try
			{
				var products = httpClient.GetFromJsonAsync<IEnumerable<ProductDto>>("api/Product");
				return products;
			}
			catch (Exception)
			{

				throw;
			}
        }
    }
}
