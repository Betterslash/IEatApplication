using Infrastructure.Data.Context;
using Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using OutgoingContext.Food.Domain;
using OutgoingContext.Food.Repositories;
using OutgoingContext.Product.Constants;
using OutgoingContext.Product.Projections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class ProductRepository : Repository, IProductRepository
    {
        public ProductRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
        }

        public void AddProduct(ProductModel product)
        {
            AllSqlEntities<ProductDAO>().Add(MapFromModelToEntity(product));
            SaveChanges();
        }

        public IEnumerable<ProductDetailProjection> GetAllProductProjections() => AllSqlEntities<ProductDAO>()
            .AsNoTracking()
            .Select(MapFromEntityToProjection)
            .AsEnumerable();

        public IEnumerable<ProductModel> GetAllProducts() => AllSqlEntities<ProductDAO>()
            .AsNoTracking()
            .Select(MapFromEntityToModel)
            .AsEnumerable();

        public void RemoveProductById(Guid productId)
        {
            var product = AllSqlEntities<ProductDAO>().First(e => e.Id == productId);
            product.State = ProductState.Removed;
            AllSqlEntities<ProductDAO>().Update(product);
            SaveChanges();
        }

        public void UpdateProducts(ProductModel product)
        {
            AllSqlEntities<ProductDAO>().Update(MapFromModelToEntity(product));
            SaveChanges();
        }

        private ProductModel MapFromEntityToModel(ProductDAO entity) => new()
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            Price = entity.Price,
            Type = entity.Type,
            State = entity.State
        };

        private static ProductDAO MapFromModelToEntity(ProductModel model) => new()
        {
            Id = model.Id,
            Name = model.Name,
            Description = model.Description,
            Price = model.Price,
            Type = model.Type,
            State = model.State
        };
        private ProductDetailProjection MapFromEntityToProjection(ProductDAO entity) => new()
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            Price = entity.Price,
            Type = entity.Type,
            State = entity.State
        };
    }
}
