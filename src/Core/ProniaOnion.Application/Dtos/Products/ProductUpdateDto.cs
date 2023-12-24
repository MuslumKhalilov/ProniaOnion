using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion.Application.Dtos.Products
{
    public record ProductUpdateDto(string name, string SKU, string? Description, decimal price);
    
    
}
