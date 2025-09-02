namespace domain.dtos.product
{
    public class ProductDetailDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Supplier { get; set; }
        public string SupplierCNPJ { get; set; }
        public decimal PriceCost { get; set; }
        public decimal PriceSale { get; set; }
        public int CurrentStock { get; set; }
    }
}
