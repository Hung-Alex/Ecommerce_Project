﻿using Application.Common.Interface;
using Application.Features.Carts.Specification;
using Application.Features.Products.Specification;
using Domain.Constants;
using Domain.Entities.Carts;
using Domain.Entities.Products;
using Domain.Shared;
using FluentValidation;
using MediatR;

namespace Application.Features.Carts.Commands.AddItem
{
    public sealed class AddItemCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<AddItemCommand, Result<bool>>
    {
        public class AddItemCommandValidator : AbstractValidator<AddItemCommand>
        {
            public AddItemCommandValidator()
            {
                RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Quantity Must Be Greater Than 0");
            }
        }
        public async Task<Result<bool>> Handle(AddItemCommand request, CancellationToken cancellationToken)
        {
            var repo = unitOfWork.GetRepository<Cart>();
            var cart = await repo.FindOneAsync(new GetCartByUserIdSpecification(request.UserId));
            if (cart is null)
            {
                return Result<bool>.ResultFailures(ErrorConstants.CartError.CartNotFound);
            }
            var repoProduct = unitOfWork.GetRepository<Product>();
            var product = await repoProduct.FindOneAsync(new GetProductWithVariantsSpecification(request.ProductId));
            if (product is null) { return Result<bool>.ResultFailures(ErrorConstants.CartError.CartNotFound); }
            if (product.IsStock is false)
            {
                return Result<bool>.ResultFailures(ErrorConstants.CartError.ProductOutOfStock);
            }
            var item = cart.CreateCartItem(request.ProductId, request.Quantity);
            cart.AddItems(item);
            await unitOfWork.CommitAsync();
            return Result<bool>.ResultSuccess(true);
        }
    }
}
