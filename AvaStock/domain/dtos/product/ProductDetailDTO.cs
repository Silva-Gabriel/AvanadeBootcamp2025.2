using System.Text.Json.Serialization;

namespace domain.dtos.product
{
    public class ProductDetailDTO
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("Nome")]
        public string Name { get; set; }

        [JsonPropertyName("Descrição")]
        public string Description { get; set; }

        [JsonPropertyName("Categoria")]
        public string Category { get; set; }

        [JsonPropertyName("Fornecedor")]
        public string Supplier { get; set; }

        [JsonPropertyName("CelularFornecedor")]
        public string SupplierPhone { get; set; }

        [JsonPropertyName("LucroPorUnidade")]
        public decimal Income { get; set; }

        [JsonPropertyName("Estoque")]
        public int CurrentStock { get; set; }

        [JsonPropertyName("QuantidadeVendas")]
        public int SalesQuantity { get; set; }
    }
}
