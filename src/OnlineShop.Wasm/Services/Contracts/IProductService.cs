using OnlineShop.Shared.Dtos;

namespace OnlineShop.Wasm.Services.Contracts
{
    public interface IProductService
    {
        Task<ProductDto?> GetProductItem(int id);
        Task<IEnumerable<ProductDto>> GetProducts();
    }
}
