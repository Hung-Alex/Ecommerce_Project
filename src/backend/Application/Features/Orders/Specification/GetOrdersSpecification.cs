using Application.DTOs.Filters.Orders;
using Application.Utils;
using Domain.Entities.Orders;
using Domain.Specifications;
using System.Linq.Expressions;

namespace Application.Features.Orders.Specification
{
    public class GetOrdersSpecification : BaseSpecification<Order>
    {
        private readonly OrderFilter _filter;
        public GetOrdersSpecification(OrderFilter filter)
        {
            _filter = filter;
            Handler();
        }
        public override Expression<Func<Order, bool>> Criteria =>
           p => (string.IsNullOrEmpty(_filter.Search) || p.ShipAddress.Name.ToLower().Contains(_filter.Search.ToLower())) &&
         (string.IsNullOrEmpty(_filter.ShipAddress) || p.ShipAddress.Address.ToLower().Contains(_filter.ShipAddress.ToLower())) &&
         (_filter.Status.HasValue && _filter.Status != Guid.Empty ? p.StatusId == _filter.Status : true);
        protected override void Handler()
        {
            AddIncludeString("OrderItems.Product");
            AddIncludeString("Payment.Status");
            AddInclude(x => x.ShipAddress);
            AddInclude(x => x.Status);
            ApplyPaging(_filter.PageSize, _filter.PageNumber);
            if (PredicatedProperty.IsExitedProperty<Order>(_filter.SortColoumn))
            {
                var property = PredicatedProperty.BuildProperty<Order>(_filter.SortColoumn);
                switch (_filter.SortBy)
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

    }
}
