using OutgoingContext.Food.Domain;
using OutgoingContext.Product.Projections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutgoingContext.Food.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<ProductModel> GetAllProducts();
        IEnumerable<ProductDetailProjection> GetAllProductProjections();
        void AddProduct(ProductModel product);
        void UpdateProducts(ProductModel product);
        void RemoveProductById(Guid foodId);
    }
}
