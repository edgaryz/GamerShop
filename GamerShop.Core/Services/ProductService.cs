using GamerShop.Core.Contracts;
using GamerShop.Core.Models;

namespace GamerShop.Core.Services
{
    public class ProductService : IProductService
    {
        public readonly IProductRepository _productDbRepository;
        public ProductService(IProductRepository productDbRepository)
        {
            _productDbRepository = productDbRepository;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _productDbRepository.GetAllProducts();
        }

        public async Task<int> GetTotalProductCount()
        {
            return await _productDbRepository.GetTotalProductCount();
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _productDbRepository.GetProductById(id);
        }

        public async Task CreateProduct(Product product)
        {
            await _productDbRepository.CreateProduct(product);
        }

        public async Task UpdateProduct(int id, Product updatedProduct)
        {
            await _productDbRepository.UpdateProduct(id, updatedProduct);
        }

        public async Task DeleteProduct(int id)
        {
            await _productDbRepository.DeleteProduct(id);
        }
    }
}
