namespace ETrade.Application.DTOs.Common
{
    public class BaseDTO
    {
        public Guid Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}
