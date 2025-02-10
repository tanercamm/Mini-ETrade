using ETrade.Application.DTOs.Order;

namespace ETrade.Application.DTOs.Customer
{
    public class CustomerDTO : CustomerBaseDTO
    {
        public ICollection<OrderBaseDTO> Orders { get; set; }
    }
}
