using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProniaOnion.Application.Abstractions.Repositories;
using ProniaOnion.Domain.Entities;
using ProniaOnion.Persistance.Contexts;
using ProniaOnion.Persistance.Implementations.Repositories.Generic;

namespace ProniaOnion.Persistance.Implementations.Repositories
{
    internal class ProductRepository:Repository<Product>,IProductRepository
    {
        public ProductRepository(AppDbContext context):base(context) 
        {
            
        }
    }
}
