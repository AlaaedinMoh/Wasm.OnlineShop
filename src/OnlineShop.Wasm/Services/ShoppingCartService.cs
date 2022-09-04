using OnlineShop.Shared.Dtos;
using OnlineShop.Wasm.Services.Contracts;
using System.Net;
using System.Net.Http.Json;

namespace OnlineShop.Wasm.Services
{
    public class ShoppingCartService : ServiceBase, IShoppingCartService
    {
        private readonly HttpClient httpClient;

        public ShoppingCartService(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task<CartItemDto> AddCartItem(CartItemToAddDto cartItemToAdd)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync<CartItemToAddDto>("api/ShoppingCart", cartItemToAdd);
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode.Equals(HttpStatusCode.NoContent))
                    {
                        return default(CartItemDto);
                    }
                    return await response.Content.ReadFromJsonAsync<CartItemDto>();
                }
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"Http status:{response.StatusCode} Message: {message}");
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public Task<IEnumerable<CartItemDto>> GetItems(int userId)
        {
            try
            {
                return SendRequest<IEnumerable<CartItemDto>>($"api/{userId}/GetItems");
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
