using GamerShop.Core.Contracts;
using GamerShop.Core.Models;

namespace GamerShop.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderDbRepository;
        private readonly IMongoDbRepository _orderMongoDbRepository;
        public OrderService(IOrderRepository OrderDbRepository, IMongoDbRepository orderMongoDbRepository)
        {
            _orderDbRepository = OrderDbRepository;
            _orderMongoDbRepository = orderMongoDbRepository;
        }

        public async Task<List<Order>> GetAllOrders()
        {
            List<Order> orderList;

            if (await _orderDbRepository.GetTotalOrderCount() != await _orderMongoDbRepository.GetOrderCount())
            {
                await _orderMongoDbRepository.ClearOrderCache();
                orderList = await _orderDbRepository.GetAllOrders();
                await _orderMongoDbRepository.InsertOrderList(orderList);
            }
            else
            {
                orderList = await _orderMongoDbRepository.GetAllOrders();
            }

            return orderList;
        }

        public async Task<int> GetTotalOrderCount()
        {
            return await _orderDbRepository.GetTotalOrderCount();
        }

        public async Task<Order> GetOrderById(int id)
        {
            var order = await _orderMongoDbRepository.GetOrderById(id);

            if (order != null)
            {
                return order;
            }

            order = await _orderDbRepository.GetOrderById(id);
            await _orderMongoDbRepository.InsertOrder(order);
            return order;
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
