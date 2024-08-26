using GamerShop.Core.Contracts;
using GamerShop.Core.Models;
using MongoDB.Driver;

namespace GamerShop.Core.Repositories
{
    public class MongoDbRepository : IMongoDbRepository
    {
        private IMongoCollection<User> _users;
        private IMongoCollection<Product> _products;
        private IMongoCollection<Order> _orders;
        public MongoDbRepository(IMongoClient mongoClient)
        {
            _users = mongoClient.GetDatabase("users").GetCollection<User>("users");
            _products = mongoClient.GetDatabase("products").GetCollection<Product>("products");
            _orders = mongoClient.GetDatabase("orders").GetCollection<Order>("orders");
        }

        //User
        public async Task<List<User>> GetAllUsers()
        {
            return await _users.Find<User>(x => true).ToListAsync();
        }

        public async Task<User> GetUserById(int id)
        {
            try
            {
                return (await _users.FindAsync<User>(x => x.Id == id)).First();
            }
            catch
            {
                return null;
            }
        }

        public async Task InsertUserList(List<User> user)
        {
            await _users.InsertManyAsync(user);
        }

        public async Task InsertUser(User user)
        {
            await _users.InsertOneAsync(user);
        }

        public async Task<long> GetUserCount()
        {
            return await _users.CountDocumentsAsync(x => true);
        }

        public async Task ClearUserCache()
        {
            await _users.Database.DropCollectionAsync("users");
        }

        //Product
        public async Task<List<Product>> GetAllProducts()
        {
            return await _products.Find<Product>(x => true).ToListAsync();
        }

        public async Task<Product> GetProductById(int id)
        {
            try
            {
                return (await _products.FindAsync<Product>(x => x.Id == id)).First();
            }
            catch
            {
                return null;
            }
        }

        public async Task InsertProductList(List<Product> products)
        {
            await _products.InsertManyAsync(products);
        }

        public async Task InsertProduct(Product product)
        {
            await _products.InsertOneAsync(product);
        }

        public async Task<long> GetProductCount()
        {
            return await _products.CountDocumentsAsync(x => true);
        }

        public async Task ClearProductCache()
        {
            await _products.Database.DropCollectionAsync("products");
        }

        //Order
        public async Task<List<Order>> GetAllOrders()
        {
            return await _orders.Find<Order>(x => true).ToListAsync();
        }

        public async Task<Order> GetOrderById(int id)
        {
            try
            {
                return (await _orders.FindAsync<Order>(x => x.OrderId == id)).First();
            }
            catch
            {
                return null;
            }
        }

        public async Task InsertOrderList(List<Order> orders)
        {
            await _orders.InsertManyAsync(orders);
        }

        public async Task InsertOrder(Order order)
        {
            await _orders.InsertOneAsync(order);
        }

        public async Task<long> GetOrderCount()
        {
            return await _orders.CountDocumentsAsync(x => true);
        }

        public async Task ClearOrderCache()
        {
            await _orders.Database.DropCollectionAsync("orders");
        }
    }
}
