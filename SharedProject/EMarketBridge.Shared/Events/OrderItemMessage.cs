namespace EMarketBridge.Shared.Events
{
    public class OrderItemMessage
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public int Count { get; set; }
    }
}