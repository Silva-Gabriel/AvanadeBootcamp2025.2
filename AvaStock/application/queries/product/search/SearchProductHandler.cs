using domain.dtos.product;
using domain.interfaces.repository.read.product;
using MediatR;

namespace application.queries.product.search
{
    public class SearchProductHandler : IRequestHandler<SearchProductRequest, SearchProductResponse>
    {
        private readonly IProductRepositoryQuery Repository;

        public SearchProductHandler(IProductRepositoryQuery repository)
        {
            Repository = repository;
        }

        public async Task<SearchProductResponse> Handle(SearchProductRequest request, CancellationToken cancellationToken)
        {
            var products = await Repository.SearchProductAsync(request);

            return new SearchProductResponse
            {
                Products = [.. products]
            };
        }
    }
}