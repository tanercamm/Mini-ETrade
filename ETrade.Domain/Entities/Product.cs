using ETrade.Domain.Entities.Common;

namespace ETrade.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }

        public int Stock { get; set; }

        public decimal Price { get; set; }

        public ICollection<OrderProduct> Orders { get; set; }
    }
}
