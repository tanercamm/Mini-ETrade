namespace ETrade.Application.DTOs.Order
{
    public class CreateOrderDTO
    {
        public string Description { get; set; }
        public string Address { get; set; }

        public Guid CustomerId { get; set; }
    }
}
