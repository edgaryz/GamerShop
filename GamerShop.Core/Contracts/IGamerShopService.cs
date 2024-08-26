using GamerShop.Core.Models;

namespace GamerShop.Core.Contracts
{
    public interface IGamerShopService
    {
        //User
        Task<List<User>> GetAllUsers();
        Task<int> GetTotalUserCount();
        Task<User> GetUserById(int id);
        Task CreateUser(User user);
        Task UpdateUser(int id, User updatedUser);
        Task DeleteUser(int id);

        //Product
        Task<List<Product>> GetAllProducts();
        Task<int> GetTotalProductCount();
        Task<Product> GetProductById(int id);
        Task CreateProduct(Product product);
        Task UpdateProduct(int id, Product updatedProduct);
        Task DeleteProduct(int id);

        //Order
        Task<List<Order>> GetAllOrders();
        Task<int> GetTotalOrderCount();
        Task<Order> GetOrderById(int id);
        Task CreateOrder(Order order);
        Task UpdateOrder(int id, Order updatedOrder);
        Task DeleteOrder(int id);
    }
}
