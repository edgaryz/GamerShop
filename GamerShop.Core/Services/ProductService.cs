using GamerShop.Core.Contracts;
using GamerShop.Core.Models;

namespace GamerShop.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productDbRepository;
        private readonly IMongoDbRepository _productMongoDbRepository;
        public ProductService(IProductRepository productDbRepository, IMongoDbRepository productMongoDbRepository)
        {
            _productDbRepository = productDbRepository;
            _productMongoDbRepository = productMongoDbRepository;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            List<Product> productList;

            if (await _productDbRepository.GetTotalProductCount() != await _productMongoDbRepository.GetProductCount())
            {
                await _productMongoDbRepository.ClearProductCache();
                productList = await _productDbRepository.GetAllProducts();
                await _productMongoDbRepository.InsertProductList(productList);
            }
            else
            {
                productList = await _productMongoDbRepository.GetAllProducts();
            }

            return productList;
        }

        public async Task<int> GetTotalProductCount()
        {
            return await _productDbRepository.GetTotalProductCount();
        }

        public async Task<Product> GetProductById(int id)
        {
            var product = await _productMongoDbRepository.GetProductById(id);

            if (product != null)
            {
                return product;
            }

            product = await _productDbRepository.GetProductById(id);
            await _productMongoDbRepository.InsertProduct(product);
            return product;
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
