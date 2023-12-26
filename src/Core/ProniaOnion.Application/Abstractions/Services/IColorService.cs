using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProniaOnion.Application.Dtos.Color;
using ProniaOnion.Application.Dtos.Tags;

namespace ProniaOnion.Application.Abstractions.Services
{
    public interface IColorService
    {
        Task<ICollection<ColorItemDto>> GetAllAsync(int page, int take);
        //Task<GetCategoryDto> GetByIdAsync(int id);
        Task CreateAsync(ColorCreateDto categoryDto);
        Task UpdateAsync(int id, ColorUpdateDto dto);
    }
}
