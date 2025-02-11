namespace ETrade.Application.DTOs.Order
{
    public class UpdateOrderDTO : CreateOrderDTO
    {
        public Guid Id { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}
