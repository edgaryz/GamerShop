using GamerShop.Core.Contracts;
using GamerShop.Core.Models;

namespace GamerShop.Core.Services
{
    public class OrderService : IOrderService
    {
        public readonly IOrderRepository _orderDbRepository;
        public OrderService(IOrderRepository OrderDbRepository)
        {
            _orderDbRepository = OrderDbRepository;
        }

        public async Task<List<Order>> GetAllOrders()
        {
            return await _orderDbRepository.GetAllOrders();
        }

        public async Task<int> GetTotalOrderCount()
        {
            return await _orderDbRepository.GetTotalOrderCount();
        }

        public async Task<Order> GetOrderById(int id)
        {
            return await _orderDbRepository.GetOrderById(id);
        }

        public async Task CreateOrder(Order order)
        {
            await _orderDbRepository.CreateOrder(order);
        }

        public async Task UpdateOrder(int id, Order updatedOrder)
        {
            await _orderDbRepository.UpdateOrder(id, updatedOrder);
        }

        public async Task DeleteOrder(int id)
        {
            await _orderDbRepository.DeleteOrder(id);
        }
    }
}
