using OnlineShop.Shared.Dtos;

namespace OnlineShop.Wasm.Services.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProducts();
    }
}
