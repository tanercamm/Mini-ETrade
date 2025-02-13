﻿using ETrade.Domain.Repositories.Customer;
using Microsoft.EntityFrameworkCore;

namespace ETrade.Infrastructure.Repositories.Customer
{
    public class CustomerWriteRepository : WriteRepository<Domain.Entities.Customer>, ICustomerWriteRepository
    {
        public CustomerWriteRepository(DbContext context) : base(context)
        {
        }
    }
}
