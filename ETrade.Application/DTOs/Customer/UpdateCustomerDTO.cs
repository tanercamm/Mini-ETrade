namespace ETrade.Application.DTOs.Customer
{
    public class UpdateCustomerDTO : CreateCustomerDTO
    {
        public Guid Id { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}
