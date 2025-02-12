namespace ETrade.Domain.Entities.Common
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }
    }


}
