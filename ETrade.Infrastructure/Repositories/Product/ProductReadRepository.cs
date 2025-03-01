﻿using ETrade.Domain.Repositories.Product;
using Microsoft.EntityFrameworkCore;

namespace ETrade.Infrastructure.Repositories.Product
{
    public class ProductReadRepository : ReadRepository<Domain.Entities.Product>, IProductReadRepository
    {
        public ProductReadRepository(DbContext context) : base(context)
        {
        }
    }
}
