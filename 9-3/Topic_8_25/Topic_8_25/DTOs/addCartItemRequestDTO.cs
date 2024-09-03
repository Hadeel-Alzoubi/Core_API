using WepAPICore.Models;

namespace WepAPICore.DTOs
{
    public class addCartItemRequestDTO
    {
        public int? CartId { get; set; }

        public int? ProductId { get; set; }

        public int Quantity { get; set; }

    }
}
