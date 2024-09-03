using WepAPICore.Models;

namespace Topic_8_25.DTOs
{
    public class categoryRequestDTO
    {
        public string? CategoryName { get; set; }

        public IFormFile? CategoryImage { get; set; }

    }
}
