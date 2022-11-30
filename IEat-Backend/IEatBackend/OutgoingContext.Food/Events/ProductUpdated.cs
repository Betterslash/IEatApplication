using Core.Events;
using OutgoingContext.Food.Domain;

namespace OutgoingContext.Product.Events
{
    public class ProductUpdated : DomainEvent
    {
        public ProductModel Product { get; set; }
        public override string AggregateName => nameof(Product);
    }
}
