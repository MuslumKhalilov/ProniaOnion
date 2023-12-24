using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProniaOnion.Application.Dtos.Categories;

namespace ProniaOnion.Application.Dtos.Products
{
    public record ProductGetDto(int id, string name, string SKU, string? Description, decimal price,IncludeCategoryDto Category,int CategoryId);
    
    
}
