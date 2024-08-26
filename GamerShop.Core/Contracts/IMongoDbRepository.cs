using GamerShop.Core.Models;

namespace GamerShop.Core.Contracts
{
    public interface IMongoDbRepository
    {
        //User
        Task<List<User>> GetAllUsers();
        Task<User> GetUserById(int id);
        Task InsertUserList(List<User> user);
        Task InsertUser(User user);
        Task<long> GetUserCount();
        Task ClearUserCache();

        //Product
        Task<List<Product>> GetAllProducts();
        Task<Product> GetProductById(int id);
        Task InsertProductList(List<Product> products);
        Task InsertProduct(Product product);
        Task<long> GetProductCount();
        Task ClearProductCache();

        //Order
        Task<List<Order>> GetAllOrders();
        Task<Order> GetOrderById(int id);
        Task InsertOrderList(List<Order> orders);
        Task InsertOrder(Order order);
        Task<long> GetOrderCount();
        Task ClearOrderCache();
    }
}
