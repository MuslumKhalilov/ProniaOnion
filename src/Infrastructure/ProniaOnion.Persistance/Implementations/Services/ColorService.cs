using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ProniaOnion.Application.Abstractions.Repositories;
using ProniaOnion.Application.Abstractions.Services;
using ProniaOnion.Application.Dtos.Color;
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
            if (!result) throw new Exception("Color doesn't exist");
            await _repository.AddAsync(color);
            await _repository.SaveChangesAsync();
        }

        public Task<ICollection<ColorItemDto>> GetAllAsync(int page, int take)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(int id, ColorUpdateDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
