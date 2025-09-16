using domain.interfaces.repository.read.product;
using MediatR;

namespace application.queries.product.details
{
    public class GetProductDetailsHandler(IProductRepositoryQuery repository) : IRequestHandler<GetProductDetailsRequest, GetProductDetailsResponse>
    {
        private readonly IProductRepositoryQuery Repository = repository;

        public async Task<GetProductDetailsResponse> Handle(GetProductDetailsRequest request, CancellationToken cancellationToken)
        {
            var product = await Repository.GetDetailByIdAsync(request.ProductId);

            if (product == null)
                return null;

            return new GetProductDetailsResponse
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Category = product.Category,
                Supplier = product.Supplier,
                SupplierPhone = product.SupplierPhone,
                Income = product.Income,
                CurrentStock = product.CurrentStock,
                SalesQuantity = product.SalesQuantity
            };
        }
    }
}