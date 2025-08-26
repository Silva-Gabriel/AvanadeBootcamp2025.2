namespace domain.entities.product
{
    public class StockHistoryEntity
    {
        private long HistoryId { get; set; }

        private long ProductId { get; set; }

        // Vincular ao sistema de vendas, ex: id pedido
        private long ReferenceId { get; set; }

        private int Type { get; set; }

        private int Quantity { get; set; }

        private DateTime CreatedAt { get; set; }

        private int Origin { get; set; }
    }
}