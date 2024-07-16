using Application.DTOs.Internal.Product;
using Application.DTOs.Responses.Banners;
using Application.DTOs.Responses.Brands;
using Application.DTOs.Responses.Category;
using Application.DTOs.Responses.Post;
using Application.DTOs.Responses.Product.Client;
using Application.DTOs.Responses.Product.Shared.BrandProduct;
using Application.DTOs.Responses.Product.Shared.CategoryProduct;
using Application.DTOs.Responses.Product.Shared.Variants;
using Application.DTOs.Responses.Rattings;
using AutoMapper;
using Domain.Entities.Banners;
using Domain.Entities.Brands;
using Domain.Entities.Category;
using Domain.Entities.Posts;
using Domain.Entities.Products;
using Domain.Entities.Rattings;

namespace Application.Mapper
{
    public class ConfigProfile : Profile
    {
        public ConfigProfile()
        {
            CreateMap<Categories, CategoryDTO>().ReverseMap();
            CreateMap<ProductInternal, ProductDTO>().ReverseMap();
            CreateMap<Brand, BrandDTO>().ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Ratting, RattingDTO>().ReverseMap();
            CreateMap<Banner, BannerDTO>().ReverseMap();
            CreateMap<Post, PostDetailDTO>();
            CreateMap<CategoryProductDTO, Categories>().ReverseMap();
            CreateMap<BrandProductDTO, Brand>().ReverseMap();
            CreateMap<ProductSkus, VariantsDTO>().ReverseMap();
        }
    }
}
