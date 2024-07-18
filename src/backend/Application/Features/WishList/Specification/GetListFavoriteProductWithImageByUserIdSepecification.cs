using Application.DTOs.Filters.WishList;
using Application.Utils;
using Domain.Entities.WishLists;
using Domain.Specifications;
using System.Linq.Expressions;

namespace Application.Features.WishsList.Specification
{
    public class GetListFavoriteProductWithImageByUserIdSepecification : BaseSpecification<WishList>
    {
        private readonly Guid _userId;
        private readonly WishListFilter _filter;
        public GetListFavoriteProductWithImageByUserIdSepecification(WishListFilter filter, Guid userId)
        {
            _userId = userId;
            _filter = filter;
            Handler();
        }
        protected override void Handler()
        {
            AddIncludeString("Product.Images");
            AddIncludeString("Product.Category");
            AddIncludeString("Product.Brand");
            AddIncludeString("Product.Rattings");
            ApplyPaging(_filter.PageSize, _filter.PageNumber);
            if (PredicatedProperty.IsExitedProperty<WishList>(_filter.SortColoumn))
            {
                var property = PredicatedProperty.BuildProperty<WishList>(_filter.SortColoumn);
                switch (_filter.SortBy.ToUpper())
                {
                    case "ASC":
                        ApplyOrderBy(property);
                        break;
                    case "DESC":
                        ApplyOrderByDescending(property);
                        break;
                    default:
                        ApplyOrderBy(b => b.Id);
                        break;
                }
            }
            else
            {
                ApplyOrderBy(b => b.Id);
            }
            base.Handler();
        }
        public override Expression<Func<WishList, bool>> Criteria => x => x.UserId == _userId;
    }
}
