using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ProniaOnion.Application.Dtos.Categories;
using ProniaOnion.Application.Dtos.Tags;
using ProniaOnion.Domain.Entities;

namespace ProniaOnion.Application.MappingProfiles
{
    public class CategoryProfile:Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category,CategoryItemDto>().ReverseMap();
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<CategoryUpdateDto, Category>().ReverseMap();
            CreateMap<Tag, TagItemDto>().ReverseMap();
            CreateMap<TagCreateDto, Tag>();
            CreateMap<TagUpdateDto, Tag>().ReverseMap();
        }
    }
}
