using Core.Commands;
using MediatR;
using OutgoingContext.Product.Projections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutgoingContext.Product.Commands
{
    public class UpdateProductCommand: ICommand<UpdateProductCommandResponse>
    {
        public ProductDetailProjection ProductDetail { get; set; }
    }
    public class UpdateProductCommandResponse : ICommandResponse
    {
        public Guid ProductId { get; set; }
    }
}
