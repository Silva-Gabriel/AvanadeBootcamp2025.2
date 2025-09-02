namespace domain.dtos.product
{
    public class SearchProductDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Supplier { get; set; }
    }
}
