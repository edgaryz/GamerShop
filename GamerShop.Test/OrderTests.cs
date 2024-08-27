using GamerShop.Core.Contracts;
using GamerShop.Core.Enums;
using GamerShop.Core.Models;
using GamerShop.Core.Services;
using Moq;

namespace GamerShop.Test
{
    public class OrderTests
    {
        [Fact]
        public async Task GetorderListTest()
        {
            //Arrange
            Mock<IOrderRepository> _ordersRepository = new Mock<IOrderRepository>();
            Mock<IMongoDbRepository> _ordersMongoRepository = new Mock<IMongoDbRepository>();

            List<Order> orderList = new List<Order>();

            Product product1 = new Product
            {
                Id = 15,
                ProductName = "Zelda 3",
                Price = 12.99M,
                ProductType = ProductType.PCGame,
                CountInStorage = 15
            };

            Product product2 = new Product
            {
                Id = 16,
                ProductName = "CS GO",
                Price = 9.99M,
                ProductType = ProductType.DLC,
                CountInStorage = 20
            };

            Buyer buyer2 = new Buyer
            {
                Id = 16,
                FirstName = "Edgar",
                LastName = "Juggernaut",
                Email = "Juggernaut@mail.com",
                PhoneNumber = "1568742135"
            };

            Seller seller3 = new Seller
            {
                Id = 17,
                FirstName = "Edgar",
                LastName = "Rikuda",
                Email = "Rikuda@mail.com",
                PhoneNumber = "1564487135"
            };

            Order order1 = new Order
            {
                OrderId = 15,
                Product = product1,
                User = buyer2,
                OrderDate = DateTime.Now,
                Quantity = 15
            };

            Order order2 = new Order
            {
                OrderId = 16,
                Product =product2,
                User = seller3,
                OrderDate = DateTime.Now,
                Quantity = 10
            };

            orderList.Add(order1);
            orderList.Add(order2);

            _ordersRepository.Setup(x => x.GetAllOrders().Result).Returns(orderList);
            _ordersRepository.Setup(x => x.GetTotalOrderCount().Result).Returns(orderList.Count);
            _ordersMongoRepository.Setup(x => x.GetUserCount().Result).Returns(0);
            IOrderService orderService = new OrderService(_ordersRepository.Object, _ordersMongoRepository.Object);

            //Act
            var testList = await orderService.GetAllOrders();
            //Assert
            Assert.Equal(orderList.Count, testList.Count);
        }
    }
}
