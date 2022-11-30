using Core.Events;
using OutgoingContext.Food.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutgoingContext.Product.Events
{
    public class ProductRegistered : DomainEvent
    {
        public override string AggregateName => nameof(Product);

        public ProductModel Product{get; set;}
    }
}
