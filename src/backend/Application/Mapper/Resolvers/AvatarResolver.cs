using Application.Common.Interface;
using AutoMapper;
using Domain.Common;
using Domain.Shared;


namespace Application.Mapper.Resolvers
{
    public class AvatarResolver : IValueResolver<ICreatedAndUpdatedBy, IHasImageDTO, string>
    {
        private readonly IMedia _media;
        public AvatarResolver(IMedia media)
        {
            _media = media;
        }
        public string Resolve(ICreatedAndUpdatedBy source, IHasImageDTO destination, string destMember, ResolutionContext context)
        {
            if (source is null)
            {
                return null;
            }
            return _media.GetUrl(source.CreatedByUser.AvatarImage);
        }
    }
}
