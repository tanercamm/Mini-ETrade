﻿using ETrade.Domain.Repositories.Order;
using Microsoft.EntityFrameworkCore;

namespace ETrade.Infrastructure.Repositories.Order
{
    public class OrderReadRepository : ReadRepository<Domain.Entities.Order>, IOrderReadRepository
    {
        public OrderReadRepository(DbContext context) : base(context)
        {
        }
    }
}
