using Application.DTOs.Responses.Brand;
using Application.DTOs.Responses.Category;
using Application.DTOs.Responses.Product;
using Application.DTOs.Responses.Rattings;
using Application.DTOs.Responses.Tags;
using AutoMapper;
using Domain.Entities.Brands;
using Domain.Entities.Category;
using Domain.Entities.Products;
using Domain.Entities.Rattings;
using Domain.Entities.Tags;

namespace Application.Mapper
{
    public class ConfigProfile : Profile
    {
        public ConfigProfile()
        {
            CreateMap<Brand, BrandDTOs>().ReverseMap();
            CreateMap<Categories, CategoryDTO>().ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Tag, TagDTO>().ReverseMap();
            CreateMap<Ratting, RattingDTO>().ReverseMap();

        }
    }
}
