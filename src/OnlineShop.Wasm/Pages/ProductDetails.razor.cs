using Microsoft.AspNetCore.Components;
using OnlineShop.Shared.Dtos;
using OnlineShop.Wasm.Services.Contracts;

namespace OnlineShop.Wasm.Pages
{
    public partial class ProductDetails
    {
        [Inject]
        private IProductService productService { get; set; }

        [Inject]
        private IShoppingCartService shoppingService { get; set; }

        private ProductDto product;

        [Parameter]
        public int ProductId { get; set; }

        public string ErrorMessage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                product = await productService.GetProductItem(ProductId);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        protected async Task AddToCartClick(CartItemToAddDto itemToAdd)
        {
            try
            {
                CartItemDto cartItem = await shoppingService.AddCartItem(itemToAdd);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
