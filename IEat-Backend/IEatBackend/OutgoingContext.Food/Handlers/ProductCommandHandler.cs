using Core.EventStoreContext;
using MediatR;
using OutgoingContext.Product.Commands;
using OutgoingContext.Product.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutgoingContext.Product.Handlers
{
    public class ProductCommandHandler : IRequestHandler<RegisterProductCommand, RegisterProductCommandResponse>,
                                         IRequestHandler<UpdateProductCommand, UpdateProductCommandResponse>,
                                         IRequestHandler<RemoveProductCommand, RemoveProductCommandResponse>
    {
        private readonly IEventStore eventStore;

        public ProductCommandHandler(IEventStore eventStore)
        {
            this.eventStore = eventStore;
        }

        public async Task<RegisterProductCommandResponse> Handle(RegisterProductCommand request, CancellationToken cancellationToken)
        {
            var aggregateId = Guid.NewGuid();
            var @event = new ProductRegistered
            {
                Id = Guid.NewGuid(),
                AggregateId = aggregateId,
                Product = new Food.Domain.ProductModel
                {
                    Id = aggregateId,
                    Name = request.Product.Name,
                    Description = request.Product.Description,
                    Price = request.Product.Price,
                    Type = request.Product.Type,
                    State = request.Product.State
                },
                Version = aggregateId + "|" + 0.ToString()
            };
            var aggregate = new Domain.Product(eventStore)
            {
                Id = aggregateId
            };
            await aggregate.ProcessEvent(@event);

            return new RegisterProductCommandResponse { ProductId = aggregate.Id };
        }

        public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var aggregateId = request.ProductDetail.Id;
            var @event = new ProductUpdated
            {
                Id = Guid.NewGuid(),
                AggregateId = aggregateId,
                Product = new Food.Domain.ProductModel
                {
                    Id = aggregateId,
                    Name = request.ProductDetail.Name,
                    Description = request.ProductDetail.Description,
                    Price = request.ProductDetail.Price,
                    Type = request.ProductDetail.Type,
                    State = request.ProductDetail.State
                },
                Version = aggregateId + "|" + 0.ToString()
            };
            var aggregate = new Domain.Product(eventStore)
            {
                Id = aggregateId
            };
            await aggregate.ProcessEvent(@event);

            return new UpdateProductCommandResponse { ProductId = aggregate.Id };
        }

        public async Task<RemoveProductCommandResponse> Handle(RemoveProductCommand request, CancellationToken cancellationToken)
        {
            var aggregateId = request.ProductId;
            var @event = new ProductRemoved
            {
                Id = Guid.NewGuid(),
                AggregateId = aggregateId,
                ProductId = request.ProductId,
                Version = aggregateId + "|" + 0.ToString()
            };
            var aggregate = new Domain.Product(eventStore)
            {
                Id = aggregateId
            };
            await aggregate.ProcessEvent(@event);

            return new RemoveProductCommandResponse {  };
        }
    }
}
