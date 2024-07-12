using Application.Common.Interface;
using Domain.Entities;
using Domain.Shared;
using FluentValidation;
using MediatR;

namespace Application.Features.Category.Commands.AddCategoryForProduct
{
    public sealed class AddCategotyForProductCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<AddCategotyForProductCommand, Result<bool>>
    {
        internal class AddCategotyForProductCommandValidator : AbstractValidator<AddCategotyForProductCommand>
        {
            public AddCategotyForProductCommandValidator()
            {
                RuleFor(x => x.ProductId).NotEmpty().WithMessage(nameof(AddCategotyForProductCommand.ProductId));
                RuleFor(x => x.ParrentCategoryId).NotEmpty().WithMessage(nameof(AddCategotyForProductCommand.ParrentCategoryId));
            }
        }

        public async Task<Result<bool>> Handle(AddCategotyForProductCommand request, CancellationToken cancellationToken)
        {
            var repo = unitOfWork.GetRepository<ProductSubCategory>();
            repo.Add(new ProductSubCategory { ProductId = request.ProductId, CategoryId = request.ParrentCategoryId });
            if (request.SubCategoryId is not null)
            {
                repo.Add(new ProductSubCategory { ProductId = request.ProductId, CategoryId = (Guid)request.SubCategoryId });
            }
            await unitOfWork.Commit();
            return Result<bool>.ResultSuccess(true);
        }
    }
}
