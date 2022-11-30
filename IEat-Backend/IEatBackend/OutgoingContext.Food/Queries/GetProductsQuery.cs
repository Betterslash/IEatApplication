using Core.Queries;
using OutgoingContext.Product.Projections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutgoingContext.Product.Queries
{
    public class GetProductsQuery: IQueryBase<GetProductsQueryResponse>
    {

    }

    public class GetProductsQueryResponse : QueryResponseBase 
    {
        public IEnumerable<ProductDetailProjection> Products { get; set; }
    }
}
