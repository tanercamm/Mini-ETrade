﻿using ETrade.Domain.Repositories.Order;
using Microsoft.EntityFrameworkCore;

namespace ETrade.Infrastructure.Repositories.Order
{
    public class OrderWriteRepository : WriteRepository<Domain.Entities.Order>, IOrderWriteRepository
    {
        public OrderWriteRepository(DbContext context) : base(context)
        {
        }
    }
}
