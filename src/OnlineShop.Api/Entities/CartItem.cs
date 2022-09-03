using OnlineShop.Api.Base;

namespace OnlineShop.Api.Entities
{
    public class CartItem : EntityBase
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
