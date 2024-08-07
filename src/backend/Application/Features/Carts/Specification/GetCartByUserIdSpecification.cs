﻿using Domain.Entities.Carts;
using Domain.Specifications;
using System.Linq.Expressions;

namespace Application.Features.Carts.Specification
{
    public class GetCartByUserIdSpecification : BaseSpecification<Cart>
    {
        private readonly Guid _userId;
        public GetCartByUserIdSpecification(Guid userId)
        {
            _userId = userId;
            AddInclude(x => x.CartItems);
        }
        public override Expression<Func<Cart, bool>> Criteria => c => c.UserId == _userId;
        
    }
}
