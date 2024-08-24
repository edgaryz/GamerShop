using GamerShop.Core.Contracts;
using GamerShop.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace GamerShop.Core.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public async Task<List<Product>> GetAllProducts()
        {
            using (var context = new MyDbContext())
            {
                List<Product> allProducts = await context.Products.ToListAsync();
                return allProducts;
            }
        }

        public async Task<int> GetTotalProductCount()
        {
            using (var context = new MyDbContext())
            {
                return await context.Products.CountAsync();
            }
        }

        public async Task<Product> GetProductById(int id)
        {
            using (var context = new MyDbContext())
            {
                return await context.Products.FindAsync(id);
            }
        }

        public async Task CreateProduct(Product product)
        {
            using (var context = new MyDbContext())
            {
                //AddAsync does not work, so we use Update
                context.Products.Update(product);
                await context.SaveChangesAsync();
            }
        }

        public async Task UpdateProduct(int id, Product updatedProduct)
        {
            using (var context = new MyDbContext())
            {
                var existingProduct = await context.Products.FindAsync(id);
                if (existingProduct != null)
                {
                    existingProduct.ProductName = updatedProduct.ProductName;
                    existingProduct.Price = updatedProduct.Price;
                    existingProduct.ProductType = updatedProduct.ProductType;
                    existingProduct.CountInStorage = updatedProduct.CountInStorage;

                    await context.SaveChangesAsync();
                }
                else
                {
                    throw new ArgumentException("Product not found");
                }
            }
        }

        public async Task DeleteProduct(int id)
        {
            using (var context = new MyDbContext())
            {
                context.Products.Remove(await context.Products.FindAsync(id));
                await context.SaveChangesAsync();
            }
        }
    }
}
