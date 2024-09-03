using WepAPICore.Models;

namespace WepAPICore.DTOs
{
    public class cartItemResponseDTO
    {
        public int CartItemId { get; set; }

        public int? CartId { get; set; }

        public int Quantity { get; set; }

        public productDTO Product { get; set; }

    }

    public class productDTO
    {
        public int ProductId { get; set; }

        public string? ProductName { get; set; }

        public int? Price { get; set; }
    }
}
