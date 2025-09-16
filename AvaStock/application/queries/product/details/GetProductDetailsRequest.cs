using MediatR;

namespace application.queries.product.details
{
    public class GetProductDetailsRequest : IRequest<GetProductDetailsResponse>
    {
        public long ProductId { get; set; }
    }
}