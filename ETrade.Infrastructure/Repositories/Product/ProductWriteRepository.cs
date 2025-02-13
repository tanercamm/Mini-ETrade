﻿using ETrade.Domain.Repositories.Product;
using Microsoft.EntityFrameworkCore;

namespace ETrade.Infrastructure.Repositories.Product
{
    public class ProductWriteRepository : WriteRepository<Domain.Entities.Product>, IProductWriteRepository
    {
        public ProductWriteRepository(DbContext context) : base(context)
        {
        }
    }
}
