using OnlineShop.Shared.Dtos;
using OnlineShop.Wasm.Services.Contracts;
using System.Net;
using System.Net.Http.Json;

namespace OnlineShop.Wasm.Services
{
    public class ProductService : ServiceBase, IProductService
    {

        public ProductService(HttpClient httpClient) : base(httpClient)
        {
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
    }
}
