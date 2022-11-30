using Core;
using Core.Events;
using Core.EventStoreContext;
using OutgoingContext.Product.Constants;
using OutgoingContext.Product.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutgoingContext.Product.Domain
{
    public class Product : AggregateRoot,
        IReconstituteHandler<ProductRegistered>,
        IReconstituteHandler<ProductUpdated>,
        IReconstituteHandler<ProductRemoved>
    {
        public Product(IEventStore eventStore) : base(eventStore)
        {
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string State { get; set; }
        public string Type { get; set; }

        public void When(ProductRegistered @event)
        {
            Name = @event.Product.Name;
            Description = @event.Product.Description;
            Price = @event.Product.Price;
        }

        public void When(ProductUpdated @event)
        {
            Name = @event.Product.Name;
            Description = @event.Product.Description;
            Price = @event.Product.Price;
            Type = @event.Product.Type;
            State = @event.Product.State;
        }

        public void When(ProductRemoved @event)
        {
            State = ProductState.Removed;
        }
    }
}
