using OnlineShop.Shared.Dtos;

namespace OnlineShop.Wasm.Services.Contracts
{
    public interface IShoppingCartService
    {
        Task<IEnumerable<CartItemDto>> GetItems(int userId);
        Task<CartItemDto> AddCartItem(CartItemToAddDto cartItemToAdd);
    }
}
