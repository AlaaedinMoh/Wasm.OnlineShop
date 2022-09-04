using Microsoft.AspNetCore.Components;

namespace OnlineShop.Wasm.Shared.Components
{
    public partial class DisplayError
    {
        [Parameter]
        public string ErrorMessage { get; set; }
    }
}
