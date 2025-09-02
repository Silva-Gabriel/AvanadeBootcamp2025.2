using MediatR;

namespace application.queries.product.details
{
    public class GetProductDetailsHandler : IRequestHandler<GetProductDetailsRequest, GetProductDetailsResponse>
    {
        public Task<GetProductDetailsResponse> Handle(GetProductDetailsRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException("Método ainda não implementado");
        }
    }
}