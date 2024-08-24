using GamerShop.Core.Models;

namespace GamerShop.Core.Contracts
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProducts();
        Task<int> GetTotalProductCount();
        Task<Product> GetProductById(int id);
        Task CreateProduct(Product product);
        Task UpdateProduct(int id, Product updatedProduct);
        Task DeleteProduct(int id);
    }
}
