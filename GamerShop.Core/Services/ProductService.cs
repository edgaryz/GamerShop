using GamerShop.Core.Contracts;
using GamerShop.Core.Models;
using Serilog;

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
                Log.Information("Got Products from DB");
            }
            else
            {
                productList = await _productMongoDbRepository.GetAllProducts();
                Log.Information("Got Products from Mongo DB");
            }

            return productList;
        }

        public async Task<int> GetTotalProductCount()
        {
            Log.Information("GetTotalProductCount called");
            return await _productDbRepository.GetTotalProductCount();
        }

        public async Task<Product> GetProductById(int id)
        {
            var product = await _productMongoDbRepository.GetProductById(id);

            if (product != null)
            {
                Log.Information($"Product with id {product.Id} found in Mongo DB");
                return product;
            }

            product = await _productDbRepository.GetProductById(id);
            await _productMongoDbRepository.InsertProduct(product);
            Log.Information($"Product with id {product.Id} found in Mongo DB");
            return product;
        }

        public async Task CreateProduct(Product product)
        {
            await _productDbRepository.CreateProduct(product);
            Log.Information("CreateProduct called");
        }

        public async Task UpdateProduct(int id, Product updatedProduct)
        {
            await _productDbRepository.UpdateProduct(id, updatedProduct);
            Log.Information("UpdateProduct called");
        }

        public async Task DeleteProduct(int id)
        {
            await _productDbRepository.DeleteProduct(id);
            Log.Information("DeleteProduct called");
        }
    }
}
