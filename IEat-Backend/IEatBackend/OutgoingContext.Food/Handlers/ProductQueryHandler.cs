using MediatR;
using OutgoingContext.Food.Repositories;
using OutgoingContext.Product.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutgoingContext.Product.Handlers
{
    public class ProductQueryHandler : IRequestHandler<GetProductsQuery, GetProductsQueryResponse>
    {
        private readonly IProductRepository productRepository;

        public ProductQueryHandler(IProductRepository productRepository) 
        {
            this.productRepository = productRepository;
        }

        public Task<GetProductsQueryResponse> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new GetProductsQueryResponse { Products = productRepository.GetAllProductProjections() });
        }
    }
}
