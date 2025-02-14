namespace ETrade.Application.DTOs.Order
{
    public class UpdateOrderDTO
    {
        public string Description { get; set; }
        public string Address { get; set; }

        public List<Guid> ProductIds { get; set; }
    }
}
