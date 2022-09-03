using Microsoft.AspNetCore.Components;
using OnlineShop.Shared.Dtos;

namespace OnlineShop.Wasm.Shared.Components
{
    public partial class DisplayProducts
    {
        [Parameter]
        public IEnumerable<ProductDto> Products { get; set; }
    }
}
