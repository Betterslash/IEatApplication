using Core.Commands;
using OutgoingContext.Food.Domain;
using OutgoingContext.Product.Projections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutgoingContext.Product.Commands
{
    public class RegisterProductCommand : ICommand<RegisterProductCommandResponse>
    {
        public ProductDetailProjection Product { get; set; }
    }

    public class RegisterProductCommandResponse : ICommandResponse
    {
        public Guid ProductId { get; set; }
    }
}
