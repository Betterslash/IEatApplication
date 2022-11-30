using Core.Events;
using MediatR;
using OutgoingContext.Food.Repositories;
using OutgoingContext.Product.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutgoingContext.Product.Handlers
{
    public class ProductEventHandler : INotificationHandler<ProductRegistered>,
                                       INotificationHandler<ProductUpdated>,
                                       INotificationHandler<ProductRemoved>
    {

        private readonly IProductRepository productRepository;

        public ProductEventHandler(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public Task Handle(ProductRegistered request, CancellationToken cancellationToken)
        {
            productRepository.AddProduct(new Food.Domain.ProductModel
            {
                Id = request.AggregateId,
                Name = request.Product.Name,
                Description = request.Product.Description,
                Price = request.Product.Price,
                State = request.Product.State,
                Type = request.Product.Type,
            });
            return Task.CompletedTask;
        }

        public Task Handle(ProductUpdated request, CancellationToken cancellationToken)
        {
            productRepository.UpdateProducts(new Food.Domain.ProductModel
            {
                Id = request.AggregateId,
                Name = request.Product.Name,
                Description = request.Product.Description,
                Price = request.Product.Price,
                State = request.Product.State,
                Type = request.Product.Type,
            });
            return Task.CompletedTask;
        }

        public Task Handle(ProductRemoved request, CancellationToken cancellationToken)
        {
            productRepository.RemoveProductById(request.AggregateId);
            return Task.CompletedTask;
        }
    }
}
