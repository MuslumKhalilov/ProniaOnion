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
        private readonly IColorRepository _colorRepository;

        public ProductService(IProductRepository repository, IMapper mapper,IColorRepository colorRepository)
        {
                _repository = repository;
                _mapper = mapper;
            _colorRepository = colorRepository;
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
            Product product = _mapper.Map<Product>(dto); 
            var result = await _repository.IsExistAsync(x => x.Name == dto.name);
            if (result)
            {
                throw new Exception("Name already exists");
            }
            var result1 = await _repository.IsExistAsync(x => x.CategoryId == dto.CategoryId);
            if (!result1)
            {
                throw new Exception("Category doesn't exist");
            }
            product.productColors= new List<ProductColor>();
            foreach (var colorId in dto.ColorIds)
            {
                if (!await _colorRepository.IsExistAsync(x=>x.Id==colorId))
                {
                    throw new Exception("Color doesn't exist");
                }
                product.productColors.Add(new ProductColor { ColorId=colorId});
            }
            await _repository.AddAsync(product);
            await _repository.SaveChangesAsync();
        }
        public async Task UpdateAsync(int id,ProductUpdateDto dto)
        {
            Product existed = await _repository.GetByIDAsync(id);
            if (existed == null) throw new Exception("Not Found");
            if (existed.CategoryId != dto.CategoryId)
                if (!await _repository.IsExistAsync(x => x.Id == dto.CategoryId)) throw new Exception("Category doesn't exist");
            existed = _mapper.Map(dto,existed);
            existed.productColors = existed.productColors.Where(pc=>dto.ColorIds.Any(colId=>pc.ColorId==colId)).ToList();
            foreach (var cId in dto.ColorIds)
            {
                if(!await _repository.IsExistAsync(x=>x.Id==cId)) throw new Exception("Color doesn't exist");
                if (existed.productColors.Any(pc => pc.ColorId == cId))
                {
                    existed.productColors.Add(new ProductColor { ColorId=cId});
                }
            }
             _repository.Update(existed);
            await _repository.SaveChangesAsync();



        }
    }
}
