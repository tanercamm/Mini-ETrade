using ETrade.Application.DTOs.Common;

namespace ETrade.Application.DTOs.Order
{
    public class OrderBaseDTO : BaseDTO
    {
        public string Description { get; set; }

        public string Address { get; set; }
    }
}
