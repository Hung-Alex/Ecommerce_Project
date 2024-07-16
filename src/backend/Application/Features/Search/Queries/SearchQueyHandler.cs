using Application.Common.Interface;
using Application.DTOs.Responses.Product.Client;
using Application.Features.Search.Specification;
using AutoMapper;
using Domain.Entities.Products;
using Domain.Shared;
using FluentValidation;
using MediatR;

namespace Application.Features.Search.Queries
{
    public sealed class SearchQueyHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<SearchQuery, Result<IEnumerable<ProductDTO>>>
    {

        internal class SearchQueyValidator : AbstractValidator<SearchQuery>
        {
            public SearchQueyValidator()
            {
                // so oh ,today , I feel so bad :))))))))))))))
            }
        }
        public async Task<Result<IEnumerable<ProductDTO>>> Handle(SearchQuery request, CancellationToken cancellationToken)
        {
            var repo = unitOfWork.GetRepository<Product>();
            var specification = new FilterProductSpecification();
            var result = await repo.GetAllAsync(specification, cancellationToken);
            return null;
        }
    }
}
