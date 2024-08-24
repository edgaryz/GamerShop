using GamerShop.Core.Contracts;
using GamerShop.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace GamerShop.Core.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        public async Task<List<Order>> GetAllOrders()
        {
            using (var context = new MyDbContext())
            {
                List<Order> allOrders = await context.Orders.ToListAsync();
                return allOrders;
            }
        }

        public async Task<int> GetTotalOrderCount()
        {
            using (var context = new MyDbContext())
            {
                return await context.Orders.CountAsync();
            }
        }

        public async Task<Order> GetOrderById(int id)
        {
            using (var context = new MyDbContext())
            {
                return await context.Orders.FindAsync(id);
            }
        }

        public async Task CreateOrder(Order order)
        {
            using (var context = new MyDbContext())
            {
                //AddAsync does not work, so we use Update
                context.Orders.Update(order);
                await context.SaveChangesAsync();
            }
        }

        public async Task UpdateOrder(int id, Order updatedOrder)
        {
            using (var context = new MyDbContext())
            {
                var existingOrder = await context.Orders.FindAsync(id);
                if (existingOrder != null)
                {
                    existingOrder.Product = updatedOrder.Product;
                    existingOrder.User = updatedOrder.User;
                    existingOrder.OrderDate = updatedOrder.OrderDate;
                    existingOrder.Quantity = updatedOrder.Quantity;

                    await context.SaveChangesAsync();
                }
                else
                {
                    throw new ArgumentException("Order not found");
                }
            }
        }

        public async Task DeleteOrder(int id)
        {
            using (var context = new MyDbContext())
            {
                context.Orders.Remove(await context.Orders.FindAsync(id));
                await context.SaveChangesAsync();
            }
        }
    }
}
