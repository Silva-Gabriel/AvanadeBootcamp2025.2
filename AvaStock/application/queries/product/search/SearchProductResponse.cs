using domain.dtos.product;
namespace application.queries.product.search
{
    public class SearchProductResponse
    {
        public List<SearchProductDTO> Products { get; set; } = [];
    }
}