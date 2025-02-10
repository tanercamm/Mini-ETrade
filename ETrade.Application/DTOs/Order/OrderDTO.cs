using ETrade.Application.DTOs.Customer;

namespace ETrade.Application.DTOs.Order
{
    public class OrderDTO : OrderBaseDTO
    {
        public CustomerBaseDTO Customer { get; set; }
    }
}
