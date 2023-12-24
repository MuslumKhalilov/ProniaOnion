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
using ProniaOnion.Application.Dtos.Tags;
using ProniaOnion.Domain.Entities;

namespace ProniaOnion.Persistance.Implementations.Services
{
    internal class TagService:ITagService
    {
        private readonly ITagRepository _repository;
        private readonly IMapper _mapper;

        public TagService(ITagRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task CreateAsync(TagCreateDto tagDto)
        {
            await _repository.AddAsync(_mapper.Map<Tag>(tagDto));
            await _repository.SaveChangesAsync();
        }

        //public async Task DeleteAsync(int id)
        //{
        //    Category category = await _repository.GetByIDAsync(id);
        //    if (category == null) throw new Exception("Not found");
        //    _repository.DeleteAsync(category);
        //    await _repository.SaveChangesAsync();

        //}

        public async Task<ICollection<TagItemDto>> GetAllAsync(int page, int take)
        {
            ICollection<Tag> tags = await _repository.GetAllWhere(skip: (page - 1) * take, take: take, isTracking: false).ToListAsync();
            ICollection<TagItemDto> tagItemDtos = _mapper.Map<ICollection<TagItemDto>>(tags);
            return tagItemDtos;
        }

        //public async Task<GetCategoryDto> GetByIdAsync(int id)
        //{
        //    Category category = await _repository.GetByIDAsync(id);
        //    if (category is null) throw new Exception("NotFound");
        //    GetCategoryDto dto = new GetCategoryDto { Name = category.Name, Id = category.Id };
        //    return dto;
        //}

        public async Task UpdateAsync(int id, TagUpdateDto dto)
        {
            Tag tag = await _repository.GetByIDAsync(id);
            if (tag is null) throw new Exception("Not found");
            _mapper.Map(dto, tag);
            await _repository.SaveChangesAsync();
        }
    }
}

