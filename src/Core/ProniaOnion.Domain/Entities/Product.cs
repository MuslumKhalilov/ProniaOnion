﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion.Domain.Entities
{
    public class Product:BaseNameableEntity
    {
        public decimal Price { get; set; }
        public string SKU { get; set; } = null!;
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }= null!;
        public ICollection<ProductColor>? productColors { get; set; }
        public ICollection<ProductTag>? productTags { get; set; }
    }
}
