using Application.DTOs.Responses.Brand;
using Application.DTOs.Responses.Category;
using Application.DTOs.Responses.Product;
using AutoMapper;
using Domain.Entities.Brands;
using Domain.Entities.Category;
using Domain.Entities.Products;

namespace Application.Mapper
{
    public class ConfigProfile : Profile
    {
        public ConfigProfile()
        {
            CreateMap<Brand, BrandDTOs>().ReverseMap();
            CreateMap<Categories, CategoryDTO>().ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap();
        }
    }
}
