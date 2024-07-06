using Application.Common.Interface;
using AutoMapper;
using MediatR;
using FluentValidation;
using Application.DTOs.Responses.Product;
using Domain.Shared;
using Domain.Constants;
using Domain.Entities.Products;

namespace Application.Features.Products.Commands.UpdateProduct
{
    public sealed class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result<ProductDTO>>
    {
        internal class UpdateBrandCommandValidator : AbstractValidator<UpdateProductCommand>
        {
            public UpdateBrandCommandValidator()
            {
                RuleFor(x => x.Id).NotEmpty().WithMessage("Not Null");
                RuleFor(b => b.Name).NotEmpty().WithMessage("Not Null");
                RuleFor(b => b.Description).NotEmpty().WithMessage("Not Null");
                RuleFor(b => b.UrlSlug).NotEmpty().WithMessage("Not Null");
            }
        }
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<ProductDTO>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var productRepo = _unitOfWork.GetRepository<Product>();
            var product = await productRepo.GetByIdAsync(request.Id);
            if (product == null) return Result<ProductDTO>.ResultFailures(ErrorConstants.NotFoundWithId(request.Id));
            product.UrlSlug = request.UrlSlug;
            product.Name = request.Name;
            product.Description = request.Description;
            product.UnitPrice = request.UnitPrice;
            product.Price = request.Price;
            product.Discount = request.Discount;
            await _unitOfWork.Commit();
            var productDTO = _mapper.Map<ProductDTO>(product);
            return Result<ProductDTO>.ResultSuccess(productDTO);
        }
    }
}
