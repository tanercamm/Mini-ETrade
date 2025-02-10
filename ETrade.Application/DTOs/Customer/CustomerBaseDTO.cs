using ETrade.Application.DTOs.Common;

namespace ETrade.Application.DTOs.Customer
{
    public class CustomerBaseDTO : BaseDTO
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }
    }
}
