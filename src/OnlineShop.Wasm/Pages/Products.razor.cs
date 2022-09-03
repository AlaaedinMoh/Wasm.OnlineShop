using Microsoft.AspNetCore.Components;
using OnlineShop.Shared.Dtos;
using OnlineShop.Wasm.Services.Contracts;

namespace OnlineShop.Wasm.Pages
{
    public partial class Products
    {
        [Inject]
        public IProductService ProductService { get; set; }

        private IEnumerable<ProductDto> products;

        protected override async Task OnInitializedAsync()
        {
            products = await ProductService?.GetProducts();
        }

        private IOrderedEnumerable<IGrouping<int, ProductDto >> GetGroupdedProductsByCategory()
        {
            return from product in products
                   group product by product.CategoryId into prodByCatGroup
                   orderby prodByCatGroup.Key
                   select prodByCatGroup;
        }

        private string GetCategoryName(IGrouping<int, ProductDto> groupedProductDtos)
        {
            return groupedProductDtos.FirstOrDefault(pg => pg.CategoryId.Equals(groupedProductDtos.Key)).CategoryName;
        }
    }
}
