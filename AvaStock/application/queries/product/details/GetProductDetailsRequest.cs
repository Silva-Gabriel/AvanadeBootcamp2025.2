using domain.dtos.product;
using domain.models.product;
using MediatR;

namespace application.queries.product.details
{
    public class GetProductDetailsRequest : ProductDetailDTO, IRequest<GetProductDetailsResponse>
    {
        
    }
}