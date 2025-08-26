namespace domain.entities.product
{
    public class ProductEntity
    {
        private long Id { get; set; }

        private long SupplierId { get; set; }

        private int CategoryId { get; set; }

        private string Name { get; set; }

        private string Description { get; set; }

        private decimal PriceCost { get; set; }

        private decimal PriceSale { get; set; }

        private int CurrentStock { get; set; }
    }
}