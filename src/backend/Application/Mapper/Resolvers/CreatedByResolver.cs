using AutoMapper;
using Domain.Common;
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
            return source.CreatedByUser?.LastName + source.CreatedByUser?.FirstName;
        }
    }
}
