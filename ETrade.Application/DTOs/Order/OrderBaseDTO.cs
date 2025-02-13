using ETrade.Application.DTOs.Common;
using ETrade.Application.DTOs.Product;

namespace ETrade.Application.DTOs.Order
{
    public class OrderBaseDTO : BaseDTO
    {
        public string Description { get; set; }

        public string Address { get; set; }

        public List<ProductBaseDTO> Products { get; set; }
    }
}
