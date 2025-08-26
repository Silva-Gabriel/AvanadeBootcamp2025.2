namespace domain.entities.product
{
    public class OrderItemSale
    {
        private long ItemId { get; set; }

        private long OrderId { get; set; }

        private long ProductId { get; set; }

        private int Quantity { get; set; }

        private decimal UnityPrice { get; set; }
    }
}