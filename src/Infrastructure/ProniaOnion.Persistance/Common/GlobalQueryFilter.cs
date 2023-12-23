using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProniaOnion.Domain.Entities;

namespace ProniaOnion.Persistance.Common
{
    internal static class GlobalQueryFilter
    {

        public static void ApplyQuery<T>(ModelBuilder ModelBuilder) where T : BaseEntity, new ()
        {
            ModelBuilder.Entity<T>().HasQueryFilter(x=>x.IsDeleted==false);
        }
        public static void ApplyQueryFilters(this ModelBuilder ModelBuilder)
        {
            ApplyQuery<Category>(ModelBuilder);
            ApplyQuery<Color>(ModelBuilder);
            ApplyQuery<Tag>(ModelBuilder);
            ApplyQuery<Product>(ModelBuilder);
            ApplyQuery<ProductColor>(ModelBuilder);
            ApplyQuery<ProductTag>(ModelBuilder);
        }
    }
}
