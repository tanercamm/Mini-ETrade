using ETrade.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace ETrade.Domain.Entities
{
    public class Order : BaseEntity
    {
        public string Description { get; set; }

        public string Address { get; set; }

        public Guid CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }

        public ICollection<OrderProduct> Products { get; set; }
    }
}
