using domain.interfaces.repository.read.product;
using System.Data;
using Dapper;
using domain.models.product.parameters;
using domain.dtos.product;

namespace infrastructure.repository.product.read
{
    public class ProductRepositoryQuery(IDbConnection con) : IProductRepositoryQuery
    {
        private readonly IDbConnection Connection = con;

        public async Task<ProductDetailDTO?> GetDetailByIdAsync(long id)
        {
            var parameters = new DynamicParameters();
            var sql = $@"
                SELECT product.Id AS [{nameof(ProductDetailDTO.Id)}],
                       product.Name AS [{nameof(ProductDetailDTO.Name)}],
                       product.Description AS [{nameof(ProductDetailDTO.Description)}],
                       category.Name AS [{nameof(ProductDetailDTO.Category)}],
                       supplier.Name AS [{nameof(ProductDetailDTO.Supplier)}],
                       supplier.Phone AS [{nameof(ProductDetailDTO.SupplierPhone)}],
                       (product.PriceSale - product.PriceCost) AS [{nameof(ProductDetailDTO.Income)}],
                       product.CurrentStock AS [{nameof(ProductDetailDTO.CurrentStock)}],
                       orderItem.Quantity AS [{nameof(ProductDetailDTO.SalesQuantity)}]
                FROM TB_PROD_PRODUCT product
                INNER JOIN TB_PROD_CATEGORY category ON product.CategoryId = category.CategoryId
                INNER JOIN TB_PROD_SUPPLIER supplier ON product.SupplierId = supplier.SupplierId
                INNER JOIN TB_PROD_ORDER orderItem ON product.Id = orderItem.ProductId
                WHERE product.Id = @Id
            ";

            parameters.Add("Id", id);

            return await Connection.QueryFirstOrDefaultAsync<ProductDetailDTO?>(sql, parameters);
        }

        public async Task<IEnumerable<SearchProductDTO>> SearchProductAsync(SearchProductParameter parameters)
        {
            var sqlBuilder = new System.Text.StringBuilder();
            sqlBuilder.Append($@"
                SELECT product.Id AS [{nameof(SearchProductParameter.Id)}],
                       product.Name AS [{nameof(SearchProductParameter.Name)}],
                       product.Description AS [{nameof(SearchProductParameter.Description)}],
                       category.Name AS [{nameof(SearchProductParameter.Category)}],
                       supplier.Name AS [{nameof(SearchProductParameter.Supplier)}]
                FROM TB_PROD_PRODUCT product
                INNER JOIN TB_PROD_CATEGORY category ON product.CategoryId = category.CategoryId
                INNER JOIN TB_PROD_SUPPLIER supplier ON product.SupplierId = supplier.SupplierId
            ");

            var whereBuilder = new System.Text.StringBuilder();
            var dynamicParams = new DynamicParameters();

            if (parameters.Id.HasValue)
            {
                whereBuilder.Append("product.Id = @Id AND ");
                dynamicParams.Add("Id", parameters.Id);
            }
            if (!string.IsNullOrWhiteSpace(parameters.Name))
            {
                whereBuilder.Append("product.Name LIKE @Name AND ");
                dynamicParams.Add("Name", $"%{parameters.Name}%");
            }
            if (!string.IsNullOrWhiteSpace(parameters.Description))
            {
                whereBuilder.Append("product.Description LIKE @Description AND ");
                dynamicParams.Add("Description", $"%{parameters.Description}%");
            }
            if (!string.IsNullOrWhiteSpace(parameters.Category))
            {
                whereBuilder.Append("category.Name LIKE @Category AND ");
                dynamicParams.Add("Category", $"%{parameters.Category}%");
            }
            if (!string.IsNullOrEmpty(parameters.Supplier))
            {
                whereBuilder.Append("supplier.Name LIKE @Supplier AND ");
                dynamicParams.Add("Supplier", $"%{parameters.Supplier}%");
            }

            if (whereBuilder.Length > 0)
            {
                // Remove o último ' AND '
                whereBuilder.Length -= 5;
                sqlBuilder.Append(" WHERE ").Append(whereBuilder);
            }

            var sql = sqlBuilder.ToString();
            return await Connection.QueryAsync<SearchProductDTO>(sql, dynamicParams);
        }
    }
}