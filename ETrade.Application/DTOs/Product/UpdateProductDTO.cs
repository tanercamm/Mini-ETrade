namespace ETrade.Application.DTOs.Product
{
    public class UpdateProductDTO : CreateProductDTO
    {
        public Guid Id { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}
