using Application.Common.Interface;
using Application.DTOs.Responses.Product;
using AutoMapper;
using Domain.Shared;
using FluentValidation;
using MediatR;

namespace Application.Features.Search.Queries
{
    public sealed class SearchQueyHandler : IRequestHandler<SearchQuery, Result<IEnumerable<ProductDTO>>>
    {

        internal class SearchQueyValidator : AbstractValidator<SearchQuery>
        {
            public SearchQueyValidator()
            {
                // so oh ,today , I feel so bad :))))))))))))))
            }
        }
        private readonly ISearchService _search;
        private readonly IMapper _mapper;
        public SearchQueyHandler(ISearchService search, IMapper mapper)
        {
            _search = search;
            _mapper = mapper;
        }
        public async Task<Result<IEnumerable<ProductDTO>>> Handle(SearchQuery request, CancellationToken cancellationToken)
        {
            var products = await _search.SearchProductAsync(request.query, cancellationToken);
            var productsDTO = _mapper.Map<IEnumerable<ProductDTO>>(products.Data);
            return new PagingResult<IEnumerable<ProductDTO>>(productsDTO, products.PageNumber, products.PageSize, products.TotalItems);

        }
    }
}
