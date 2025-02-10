using ETrade.Application.DTOs.Common;

namespace ETrade.Application.DTOs.Product
{
    public class ProductBaseDTO : BaseDTO
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Stock { get; set; }
    }
}
