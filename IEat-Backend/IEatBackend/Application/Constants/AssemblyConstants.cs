using OutgoingContext.Product.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Constants
{
    public sealed class AssemblyConstants
    {
        public static readonly string ProductAssembly = typeof(Product).Assembly.FullName;
    }
}
