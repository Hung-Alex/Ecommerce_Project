using Application.Common.Interface;
using AutoMapper;
using Domain.Common;
using Domain.Entities.Banners;
using Domain.Entities.Brands;
using Domain.Entities.Category;
using Domain.Entities.Posts;
using Domain.Shared;

namespace Application.Mapper.Resolvers
{
    public class CreatedByResolver : IValueResolver<ICreatedAndUpdatedBy, ICreatedByDTO, string>
    {
        public CreatedByResolver()
        {
        }
        public string Resolve(ICreatedAndUpdatedBy source, ICreatedByDTO destination, string destMember, ResolutionContext context)
        {
            if (source is null)
            {
                return "Unknow";
            }
            return source.CreatedByUser.LastName + source.CreatedByUser.FirstName;
        }
    }
}
