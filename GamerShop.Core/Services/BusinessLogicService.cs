using GamerShop.Core.Contracts;
using GamerShop.Core.Models;

namespace GamerShop.Core.Services
{
    public class BusinessLogicService : IBusinessLogicService
    {
        public readonly IUserService _userService;
        public readonly IProductService _productService;
        public readonly IOrderService _orderService;

        public BusinessLogicService(IUserService userService, IProductService productService, IOrderService orderService)
        {
            _userService = userService;
            _productService = productService;
            _orderService = orderService;
        }

        //User
        public async Task<List<User>> GetAllUsers()
        {
            return await _userService.GetAllUsers();
        }

        public async Task<int> GetTotalUserCount()
        {
            return await _userService.GetTotalUserCount();
        }

        public async Task<User> GetUserById(int id)
        {
            return await _userService.GetUserById(id);
        }

        public async Task CreateUser(User user)
        {
            await _userService.CreateUser(user);
        }

        public async Task UpdateUser(int id, User updatedUser)
        {
            await _userService.UpdateUser(id, updatedUser);
        }

        public async Task DeleteUser(int id)
        {
            await _userService.DeleteUser(id);
        }

        //Product
        public async Task<List<Product>> GetAllProducts()
        {
            return await _productService.GetAllProducts();
        }

        public async Task<int> GetTotalProductCount()
        {
            return await _productService.GetTotalProductCount();
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _productService.GetProductById(id);
        }

        public async Task CreateProduct(Product product)
        {
            await _productService.CreateProduct(product);
        }

        public async Task UpdateProduct(int id, Product updatedProduct)
        {
            await _productService.UpdateProduct(id, updatedProduct);
        }

        public async Task DeleteProduct(int id)
        {
            await _productService.DeleteProduct(id);
        }

        //Order
        public async Task<List<Order>> GetAllOrders()
        {
            return await _orderService.GetAllOrders();
        }

        public async Task<int> GetTotalOrderCount()
        {
            return await _orderService.GetTotalOrderCount();
        }

        public async Task<Order> GetOrderById(int id)
        {
            return await _orderService.GetOrderById(id);
        }

        public async Task CreateOrder(Order order)
        {
            await _orderService.CreateOrder(order);
        }

        public async Task UpdateOrder(int id, Order updatedOrder)
        {
            await _orderService.UpdateOrder(id, updatedOrder);
        }

        public async Task DeleteOrder(int id)
        {
            await _orderService.DeleteOrder(id);
        }
    }
}
