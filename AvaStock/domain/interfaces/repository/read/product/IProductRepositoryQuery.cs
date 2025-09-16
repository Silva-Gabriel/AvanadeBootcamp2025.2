using domain.dtos.product;
using domain.models.product.parameters;

namespace domain.interfaces.repository.read.product
{
    public interface IProductRepositoryQuery
    {
        Task<ProductDetailDTO?> GetDetailByIdAsync(long id);

        Task<IEnumerable<SearchProductDTO>> SearchProductAsync(SearchProductParameter parameters);
    }
}