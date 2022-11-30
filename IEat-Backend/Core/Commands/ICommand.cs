using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Commands
{
    public interface ICommandResponse
    { 
    }
    public interface ICommand<T> : IRequest<T> where T : ICommandResponse
    {
    }
}
