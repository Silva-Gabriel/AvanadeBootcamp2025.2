using MediatR;

namespace application.commands.product.create
{
    public class CreateProductHandler : IRequestHandler<CreateProductRequest, CreateProductResponse>
    {
        public Task<CreateProductResponse> Handle(CreateProductRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}