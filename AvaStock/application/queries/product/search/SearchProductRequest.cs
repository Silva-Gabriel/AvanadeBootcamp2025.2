using domain.models.product.parameters;
using MediatR;

namespace application.queries.product.search
{
    public class SearchProductRequest : SearchProductParameter, IRequest<SearchProductResponse>
    {

    }
}