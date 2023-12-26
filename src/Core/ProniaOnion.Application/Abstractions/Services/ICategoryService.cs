
using ProniaOnion.Application.Dtos.Categories;

namespace ProniaOnion.Application.Abstractions.Services
{
    public interface ICategoryService
    {
        Task<ICollection<CategoryItemDto>> GetAllAsync(int page, int take);
        Task<CategoryItemDto> GetByIdAsync(int id);
        Task CreateAsync(CategoryCreateDto categoryDto);
        Task UpdateAsync(int id, CategoryUpdateDto dto);
        Task SoftDeleteAsync(int id);
        Task DeleteAsync(int id);
        Task ReverseSoftDeleteAsync(int id);
    }
}
