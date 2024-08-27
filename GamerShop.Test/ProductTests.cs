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

            productList.Add(product1);
            productList.Add(product2);

            _productsRepository.Setup(x => x.GetAllProducts().Result).Returns(productList);
            _productsRepository.Setup(x => x.GetTotalProductCount().Result).Returns(productList.Count);
            _productsMongoRepository.Setup(x => x.GetProductCount().Result).Returns(0);
            IProductService productService = new ProductService(_productsRepository.Object, _productsMongoRepository.Object);

            //Act
            var testList = await productService.GetAllProducts();
            //Assert
            Assert.Equal(productList.Count, testList.Count);
        }

        [Fact]
        public async Task GetProductCountTest()
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
                ProductType = ProductType.PCGame,
                CountInStorage = 15
            };

            productList.Add(product1);

            _productsRepository.Setup(x => x.GetTotalProductCount().Result).Returns(productList.Count);
            _productsMongoRepository.Setup(x => x.GetProductCount().Result).Returns(0);
            IProductService productService = new ProductService(_productsRepository.Object, _productsMongoRepository.Object);

            //Act
            var testList = await productService.GetTotalProductCount();
            //Assert
            Assert.Equal(productList.Count, testList);
        }

        [Fact]
        public async Task GetProductByIdTest()
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

            productList.Add(product1);
            productList.Add(product2);

            _productsRepository.Setup(x => x.GetProductById(16).Result).Returns(product2);
            IProductService productService = new ProductService(_productsRepository.Object, _productsMongoRepository.Object);

            //Act
            Product testSubject = await productService.GetProductById(16);
            //Assert
            Assert.Equal(16, testSubject.Id);
        }

        [Fact]
        public async Task CreateProductTest()
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
                ProductType = ProductType.PCGame,
                CountInStorage = 15
            };

            productList.Add(product1);

            _productsRepository.Setup(x => x.CreateProduct(product1));
            _productsRepository.Setup(x => x.GetProductById(15).Result).Returns(product1);
            IProductService productService = new ProductService(_productsRepository.Object, _productsMongoRepository.Object);

            //Act
            await productService.CreateProduct(product1);
            var createdProduct = await productService.GetProductById(15);
            //Assert
            Assert.Equal("Zelda 3", createdProduct.ProductName);
        }

        [Fact]
        public async Task UpdateProductTest()
        {
            //Arrange
            Mock<IProductRepository> _productsRepository = new Mock<IProductRepository>();
            Mock<IMongoDbRepository> _productsMongoRepository = new Mock<IMongoDbRepository>();

            Product product1 = new Product
            {
                Id = 16,
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

            _productsRepository.Setup(x => x.CreateProduct(product1));
            _productsRepository.Setup(x => x.UpdateProduct(16, product2));
            _productsRepository.Setup(x => x.GetProductById(16).Result).Returns(product2);
            IProductService productService = new ProductService(_productsRepository.Object, _productsMongoRepository.Object);

            //Act
            await productService.UpdateProduct(16, product2);
            var createdProduct = await productService.GetProductById(16);
            //Assert
            Assert.Equal("CS GO", createdProduct.ProductName);
        }

        [Fact]
        public async Task DeleteProductTest()
        {
            //Arrange
            Mock<IProductRepository> _productsRepository = new Mock<IProductRepository>();
            Mock<IMongoDbRepository> _productsMongoRepository = new Mock<IMongoDbRepository>();

            Product product1 = new Product
            {
                Id = 16,
                ProductName = "Zelda 3",
                Price = 12.99M,
                ProductType = ProductType.PCGame,
                CountInStorage = 15
            };

            _productsRepository.Setup(x => x.CreateProduct(product1));
            _productsRepository.Setup(x => x.DeleteProduct(16));
            _productsRepository.Setup(x => x.GetProductById(16).Result).Returns(product1);
            IProductService productService = new ProductService(_productsRepository.Object, _productsMongoRepository.Object);

            //Act
            await productService.CreateProduct(product1);
            await productService.DeleteProduct(16);
            var testListCount = await productService.GetTotalProductCount();
            //Assert
            Assert.Equal(0, testListCount);
        }
    }
}
