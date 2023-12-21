﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task CreateAsync(CategoryCreateDto categoryDto)
        {
            await _repository.AddAsync(new Category { Name = categoryDto.Name });
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
            ICollection<CategoryItemDto> getCategoryDtos = new List<CategoryItemDto>();
            foreach (var category in categories)
            {
                getCategoryDtos.Add(new CategoryItemDto(category.Id,category.Name));
            }
            return getCategoryDtos;
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