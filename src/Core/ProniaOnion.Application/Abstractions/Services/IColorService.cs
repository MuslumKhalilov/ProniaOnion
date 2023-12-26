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
        Task<ColorItemDto> GetByIdAsync(int id);
        Task CreateAsync(ColorCreateDto categoryDto);
        Task UpdateAsync(int id, ColorUpdateDto dto);
        Task SoftDeleteAsync(int id);
        Task DeleteAsync(int id);
        Task ReverseSoftDeleteAsync(int id);
    }
}
