﻿using ETrade.Domain.Repositories.Customer;
using Microsoft.EntityFrameworkCore;

namespace ETrade.Infrastructure.Repositories.Customer
{
    public class CustomerReadRepository : ReadRepository<Domain.Entities.Customer>, ICustomerReadRepository
    {
        public CustomerReadRepository(DbContext context) : base(context)
        {
        }
    }
}
