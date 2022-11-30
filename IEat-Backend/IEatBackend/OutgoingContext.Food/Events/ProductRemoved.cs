using Core.Events;

namespace OutgoingContext.Product.Events
{
    public class ProductRemoved : DomainEvent
    {
        public Guid ProductId { get; set; }
        public override string AggregateName => nameof(Product);
    }
}
