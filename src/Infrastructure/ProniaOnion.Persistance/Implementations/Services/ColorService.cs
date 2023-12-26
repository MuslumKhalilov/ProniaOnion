using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProniaOnion.Application.Abstractions.Repositories;
using ProniaOnion.Application.Abstractions.Services;
using ProniaOnion.Application.Dtos.Color;
using ProniaOnion.Application.Dtos.Tags;
using ProniaOnion.Domain.Entities;
using ProniaOnion.Persistance.Implementations.Repositories;

namespace ProniaOnion.Persistance.Implementations.Services
{
    internal class ColorService : IColorService
    {
        private readonly IColorRepository _repository;
        private readonly IMapper _mapper;

        public ColorService(IColorRepository repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task CreateAsync(ColorCreateDto dto)
        {
            Color color = _mapper.Map<Color>(dto);
            var result = await _repository.IsExistAsync(x=>x.Name==color.Name);
            if (result) throw new Exception("Color already exist");
            await _repository.AddAsync(color);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0) throw new Exception("Id should not be negative");
            Color color = await _repository.GetByIDAsync(id);
            if (color is null) throw new Exception("Color doesn't exist");
            _repository.DeleteAsync(color);
            await _repository.SaveChangesAsync();
        }

        public async Task<ICollection<ColorItemDto>> GetAllAsync(int page, int take)
        {
            ICollection<Color> colors = await _repository.GetAllWhere(skip:(page-1)*take,take:take,isTracking:false).ToListAsync();
            ICollection<ColorItemDto> dtos = _mapper.Map<ICollection<ColorItemDto>>(colors);
            return dtos;
        }

        public async Task<ColorItemDto> GetByIdAsync(int id)
        {
            if (id <= 0) throw new Exception("Id should not be negative");
            Color color = await _repository.GetByIDAsync(id);
            if (color is null) throw new Exception("Color doesn't exist");
            ColorItemDto dto = _mapper.Map<ColorItemDto>(color);
            return dto;
        }

        public async Task ReverseSoftDeleteAsync(int id)
        {
            if (id <= 0) throw new Exception("Id should not be negative");
            Color color = await _repository.GetByIDAsync(id);
            if (color is null) throw new Exception("Color doesn't exist");
            _repository.ReverseSoftDeleteAsync(color);
            await _repository.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync(int id)
        {
            if (id <= 0) throw new Exception("Id should not be negative");
            Color color = await _repository.GetByIDAsync(id);
            if (color is null) throw new Exception("Color doesn't exist");
            _repository.SoftDeleteAsync(color);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, ColorUpdateDto dto)
        {
            if (id <= 0) throw new Exception("Id should not be negative");
            Color existed = await _repository.GetByIDAsync(id);
            _mapper.Map(dto,existed);
            await _repository.SaveChangesAsync();
        }
    }
}
