using GamerShop.Core.Models;

namespace GamerShop.Core.Contracts
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllOrders();
        Task<int> GetTotalOrderCount();
        Task<Order> GetOrderById(int id);
        Task CreateOrder(Order order);
        Task UpdateOrder(int id, Order updatedOrder);
        Task DeleteOrder(int id);
    }
}
