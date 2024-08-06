using Application.Common.Interface;
using Application.Features.Products.Specification;
using Domain.Constants;
using Domain.Entities.Products;
using Domain.Entities.Rattings;
using Domain.Shared;
using FluentValidation;
using MediatR;


namespace Application.Features.Rattings.Commands.CreateRatting
{
    public sealed class CreateRattingCommandHandler : IRequestHandler<CreateRattingCommand, Result<bool>>
    {
        internal class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommandValidator>
        {

        }
        private readonly IUnitOfWork _unitOfWork;

        public CreateRattingCommandHandler(IMedia media, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<bool>> Handle(CreateRattingCommand request, CancellationToken cancellationToken)
        {
            var repoRatting = _unitOfWork.GetRepository<Ratting>();
            var repoProduct = _unitOfWork.GetRepository<Product>();
            var product = await repoProduct.FindOneAsync(new GetProductByIdSepecification(request.ProductId));
            if (product is null)
            {
                return Result<bool>.ResultFailures(ErrorConstants.NotFoundWithId(request.ProductId));
            }
            repoRatting.Add(new Ratting()
            {
                Rate = request.Rate
                ,
                Description = request.Description
                ,
                ProductId = request.ProductId
            });
            await _unitOfWork.CommitAsync();
            return Result<bool>.ResultSuccess(true);
        }
    }
}
