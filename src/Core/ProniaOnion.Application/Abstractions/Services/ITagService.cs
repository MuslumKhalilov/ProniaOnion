using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProniaOnion.Application.Dtos.Categories;
using ProniaOnion.Application.Dtos.Tags;

namespace ProniaOnion.Application.Abstractions.Services
{
    public interface ITagService
    {
        Task<ICollection<TagItemDto>> GetAllAsync(int page, int take);
        Task<TagItemDto> GetByIdAsync(int id);
        Task CreateAsync(TagCreateDto categoryDto);
        Task UpdateAsync(int id, TagUpdateDto dto);
        Task SoftDeleteAsync(int id);
        Task DeleteAsync(int id);
        Task ReverseSoftDeleteAsync(int id);
    }
}
