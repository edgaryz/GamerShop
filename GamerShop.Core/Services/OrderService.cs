using GamerShop.Core.Contracts;
using GamerShop.Core.Models;
using Serilog;

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
                Log.Information("Got Orders from DB");
            }
            else
            {
                orderList = await _orderMongoDbRepository.GetAllOrders();
                Log.Information("Got Orders from Mongo DB");
            }

            return orderList;
        }

        public async Task<int> GetTotalOrderCount()
        {
            Log.Information("GetTotalOrderCount called");
            return await _orderDbRepository.GetTotalOrderCount();
        }

        public async Task<Order> GetOrderById(int id)
        {
            var order = await _orderMongoDbRepository.GetOrderById(id);

            if (order != null)
            {
                Log.Information($"User with id {order.OrderId} found in Mongo DB");
                return order;
            }

            order = await _orderDbRepository.GetOrderById(id);
            await _orderMongoDbRepository.InsertOrder(order);
            Log.Information($"User with id {order.OrderId} found in DB");
            return order;
        }

        public async Task CreateOrder(Order order)
        {
            await _orderDbRepository.CreateOrder(order);
            Log.Information("CreateOrder called");
        }

        public async Task UpdateOrder(int id, Order updatedOrder)
        {
            await _orderMongoDbRepository.ClearOrderCache();
            await _orderDbRepository.UpdateOrder(id, updatedOrder);
            Log.Information("UpdateOrder called");
        }

        public async Task DeleteOrder(int id)
        {
            await _orderDbRepository.DeleteOrder(id);
            Log.Information("DeleteOrder called");
        }
    }
}
