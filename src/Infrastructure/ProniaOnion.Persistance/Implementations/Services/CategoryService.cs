using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProniaOnion.Application.Abstractions.Repositories;
using ProniaOnion.Application.Abstractions.Services;
using ProniaOnion.Application.Dtos.Categories;
using ProniaOnion.Domain.Entities;

namespace ProniaOnion.Persistance.Implementations.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task CreateAsync(CategoryCreateDto categoryDto)
        {
            await _repository.AddAsync(_mapper.Map<Category>(categoryDto));
            await _repository.SaveChangesAsync();
        }

        //public async Task DeleteAsync(int id)
        //{
        //    Category category = await _repository.GetByIDAsync(id);
        //    if (category == null) throw new Exception("Not found");
        //    _repository.DeleteAsync(category);
        //    await _repository.SaveChangesAsync();

        //}

        public async Task<ICollection<CategoryItemDto>> GetAllAsync(int page, int take)
        {
            ICollection<Category> categories = await _repository.GetAllAsync(skip: (page - 1) * take, take: take, isTracking: false).ToListAsync();
            ICollection<CategoryItemDto> categoryDtos = _mapper.Map<ICollection<CategoryItemDto>>(categories);
            return categoryDtos;
        }

        //public async Task<GetCategoryDto> GetByIdAsync(int id)
        //{
        //    Category category = await _repository.GetByIDAsync(id);
        //    if (category is null) throw new Exception("NotFound");
        //    GetCategoryDto dto = new GetCategoryDto { Name = category.Name, Id = category.Id };
        //    return dto;
        //}

        public async Task UpdateAsync(int id, string name)
        {
            Category category = await _repository.GetByIDAsync(id);
            if (category is null) throw new Exception("Not found");
            category.Name = name;
            await _repository.SaveChangesAsync();
        }
    }
}
