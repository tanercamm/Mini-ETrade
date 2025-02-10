using ETrade.Application.DTOs.Order;

namespace ETrade.Application.DTOs.Product
{
    public class ProductDTO : ProductBaseDTO
    {
        public ICollection<OrderBaseDTO> Orders { get; set; }
    }
}
