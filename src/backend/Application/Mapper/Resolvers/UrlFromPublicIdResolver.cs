using Application.Common.Interface;
using AutoMapper;
using Domain.Entities.Banners;
using Domain.Entities.Brands;
using Domain.Entities.Category;
using Domain.Entities.Posts;
using Domain.Entities.Slides;
using System;
namespace Application.Mapper.Resolve
{
    public class UrlFromPublicIdResolver : IValueResolver<object, object, string>
    {
        private readonly IMedia _media;
        public UrlFromPublicIdResolver(IMedia media)
        {
            _media = media;
        }
        public string Resolve(object source, object destination, string destMember, ResolutionContext context)
        {
            string url = string.Empty;
            switch (source)
            {
                case Categories category:
                    url = _media.GetUrl(category.Image);
                    break;
                case Brand brand:
                    url = _media.GetUrl(brand.Image);
                    break;
                case Post post:
                    url = _media.GetUrl(post.ImageUrl);
                    break;
                case Slide slide:
                    url = _media.GetUrl(slide.Image);
                    break;
                case Banner banner:
                    url = _media.GetUrl(banner.LogoImageUrl);
                    break;
                default:
                    break;
            }
            return url;

        }
    }
}
