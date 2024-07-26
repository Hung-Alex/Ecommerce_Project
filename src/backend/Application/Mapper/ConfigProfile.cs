using Application.DTOs.Internal.Product;
using Application.DTOs.Responses.Banners;
using Application.DTOs.Responses.Brands;
using Application.DTOs.Responses.Category;
using Application.DTOs.Responses.Images;
using Application.DTOs.Responses.Post;
using Application.DTOs.Responses.Product.Client;
using Application.DTOs.Responses.Product.Shared.BrandProduct;
using Application.DTOs.Responses.Product.Shared.CategoryProduct;
using Application.DTOs.Responses.Product.Shared.Variants;
using Application.DTOs.Responses.Rattings;
using Application.DTOs.Responses.Sections;
using Application.DTOs.Responses.Slides;
using Application.DTOs.Responses.Users;
using Application.Mapper.Resolve;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.Banners;
using Domain.Entities.Brands;
using Domain.Entities.Category;
using Domain.Entities.Posts;
using Domain.Entities.Products;
using Domain.Entities.Rattings;
using Domain.Entities.Slides;
using Domain.Entities.Users;

namespace Application.Mapper
{
    public class ConfigProfile : Profile
    {
        public ConfigProfile()
        {
            CreateMap<Categories, CategoryDTO>().ForMember(dest => dest.Image, opt => opt.MapFrom<UrlFromPublicIdResolver>()).ReverseMap();
            CreateMap<ProductInternal, ProductDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Brand, BrandDTO>().ForMember(dest => dest.Image, opt => opt.MapFrom<UrlFromPublicIdResolver>()).ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Slide, SlideDTO>().ForMember(dest => dest.Image, opt => opt.MapFrom<UrlFromPublicIdResolver>()).ReverseMap();
            CreateMap<Ratting, RattingDTO>().ReverseMap();
            CreateMap<Banner, BannerDTO>().ForMember(dest => dest.LogoImageUrl, opt => opt.MapFrom<UrlFromPublicIdResolver>()).ReverseMap();
            CreateMap<Post, PostDetailDTO>().ForMember(dest => dest.ImageUrl, opt => opt.MapFrom<UrlFromPublicIdResolver>());
            CreateMap<CategoryProductDTO, Categories>().ReverseMap();
            CreateMap<BrandProductDTO, Brand>().ReverseMap();
            CreateMap<ProductSkus, VariantsDTO>().ReverseMap();
            CreateMap<ImageDTO, Image>().ReverseMap();
            CreateMap<CatetgorySection, Categories>().ReverseMap();

        }
    }
}
