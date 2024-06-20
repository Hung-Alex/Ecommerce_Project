using Application.DTOs.Responses.Brand;
using Application.DTOs.Responses.Category;
using AutoMapper;
using Domain.Entities.Brands;
using Domain.Entities.Category;

namespace Application.Mapper
{
    public class ConfigProfile : Profile
    {
        public ConfigProfile()
        {
            CreateMap<Brand, BrandDTOs>().ReverseMap();
            CreateMap<Categories, CategoryDTO>().ReverseMap();
        }
    }
}
