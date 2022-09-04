namespace OnlineShop.Shared.Dtos
{
    public class CartItemDto : DtoBase
    {
        public int ProductId { get; set; }
        public int CardId { get; set; }
        public string ProductName{ get; set; }
        public string ProductDescription{ get; set; }
        public string ProductImageUrl{ get; set; }
        public decimal Price{ get; set; }
        public decimal TotalPrice{ get; set; }
        public int Quantity{ get; set; }
    }
}
