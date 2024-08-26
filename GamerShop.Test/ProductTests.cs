using GamerShop.Core.Contracts;
using GamerShop.Core.Enums;
using GamerShop.Core.Models;
using GamerShop.Core.Services;
using Moq;

namespace GamerShop.Test
{
    public class ProductTests
    {
        [Fact]
        public async Task GetProductListTest()
        {
            //Arrange
            Mock<IProductRepository> _productsRepository = new Mock<IProductRepository>();
            Mock<IMongoDbRepository> _productsMongoRepository = new Mock<IMongoDbRepository>();

            List<Product> productList = new List<Product>();
            Product product1 = new Product
            {
                Id = 15,
                ProductName = "Zelda 3",
                Price = 12.99M,
                ProductType = 0,
                CountInStorage = 15
            };

            Product product2 = new Product
            {
                Id = 16,
                ProductName = "CS GO",
                Price = 9.99M,
                ProductType = ((ProductType)1),
                CountInStorage = 20
            };

            productList.Add(product1);
            productList.Add(product2);

            _productsRepository.Setup(x => x.GetAllProducts().Result).Returns(productList);
            _productsRepository.Setup(x => x.GetTotalProductCount().Result).Returns(productList.Count);
            _productsMongoRepository.Setup(x => x.GetUserCount().Result).Returns(0);
            IProductService productService = new ProductService(_productsRepository.Object, _productsMongoRepository.Object);

            //Act
            var testList = await productService.GetAllProducts();
            //Assert
            Assert.Equal(productList.Count, testList.Count);
        }
    }
}
