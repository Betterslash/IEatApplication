using Core.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutgoingContext.Product.Commands
{
    public class RemoveProductCommand : ICommand<RemoveProductCommandResponse>
    {
        public Guid ProductId { get; set; }
    }
    public class RemoveProductCommandResponse : ICommandResponse
    {
    }
}
