using domain.dtos.product;
using domain.models.product;
using domain.models.product.parameters;

namespace domain.interfaces.repository.read.product
{
    public interface IProductRepositoryQuery
    {
        Task<IEnumerable<ProductDetailDTO>> GetAllAsync();

        Task<ProductDetailDTO> GetByIdAsync(long id);

        Task<IEnumerable<SearchProductDTO>> SearchProductAsync(SearchProductParameter parameters);
    }
}