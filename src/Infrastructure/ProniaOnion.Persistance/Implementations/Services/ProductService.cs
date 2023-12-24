using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ProniaOnion.Application.Abstractions.Repositories;
using ProniaOnion.Application.Abstractions.Services;
using ProniaOnion.Application.Dtos.Products;
using ProniaOnion.Domain.Entities;

namespace ProniaOnion.Persistance.Implementations.Services
{
    internal class ProductService:IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository repository, IMapper mapper)
        {
                _repository = repository;
                _mapper = mapper;
        }
        public async Task<IEnumerable<ProductItemDto>> GetAllPaginated(int page,int take)
        {
            IEnumerable<ProductItemDto> dto = _mapper.Map<IEnumerable<ProductItemDto>>(_repository.GetAllWhere(skip:(page-1)*take,take:take,isTracking:false)).ToArray();
            return dto;

        }
        public async Task<ProductGetDto> GetByIdAsync(int id)
        {
            ProductGetDto dto = _mapper.Map<ProductGetDto>(await _repository.GetByIDAsync(id,includes:nameof(Product.Category)));
            return dto;
        }
        public async Task CreateAsync(ProductCreatedDto dto)
        {
            var result= await _repository.IsExistAsync(x=>x.Name==dto.name);
            if (result)
            {
                throw new Exception("Name already exists");
            }
            var result1 = await _repository.IsExistAsync(x=>x.CategoryId==dto.CategoryId);
            if (!result1)
            {
                throw new Exception("Category doesn't exist");
            }
            await _repository.AddAsync(_mapper.Map<Product>(dto));
            await _repository.SaveChangesAsync();
        }
    }
}
