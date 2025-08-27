using MediatR;

namespace application.queries.product.search
{
    public class SearchProductHandler : IRequestHandler<SearchProductRequest, SearchProductResponse>
    {
        public Task<SearchProductResponse> Handle(SearchProductRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException("Você não implementou essa classe ainda!");
        }
    }
}