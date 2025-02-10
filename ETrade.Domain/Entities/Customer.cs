using ETrade.Domain.Entities.Common;

namespace ETrade.Domain.Entities
{
    public class Customer : BaseEntity
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }
        
        public ICollection<Order> Orders { get; set; }
    }
}
