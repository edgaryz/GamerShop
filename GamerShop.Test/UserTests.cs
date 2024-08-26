using GamerShop.Core.Contracts;
using GamerShop.Core.Models;
using GamerShop.Core.Services;
using Moq;
using System.Runtime.Intrinsics.X86;

namespace GamerShop.Test
{
    public class UserTests
    {
        [Fact]
        public async Task GetUserListTest()
        {
            //Arrange
            Mock<IUserRepository> _usersRepository = new Mock<IUserRepository>();
            Mock<IMongoDbRepository> _usersMongoRepository = new Mock<IMongoDbRepository>();

            List<User> userList = new List<User>();
            User user1 = new User
            {
                Id = 15,
                FirstName = "Edgar",
                LastName = "Jugger",
                Email = "jugger@mail.com",
                PhoneNumber = "15687135"
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
            userList.Add(user1);
            userList.Add(buyer2);
            userList.Add(seller3);

            _usersRepository.Setup(x => x.GetAllUsers().Result).Returns(userList);
            _usersRepository.Setup(x => x.GetTotalUserCount().Result).Returns(userList.Count);
            _usersMongoRepository.Setup(x => x.GetUserCount().Result).Returns(0);
            IUserService userService = new UserService(_usersRepository.Object, _usersMongoRepository.Object);

            //Act
            var testList = await userService.GetAllUsers();
            //Assert
            Assert.Equal(userList.Count, testList.Count);
        }

        [Fact]
        public async Task GetElectricCarByIdTest()
        {
            //Arrange
            Mock<IUserRepository> _usersRepository = new Mock<IUserRepository>();
            Mock<IMongoDbRepository> _usersMongoRepository = new Mock<IMongoDbRepository>();

            List<User> userList = new List<User>();
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
            userList.Add(buyer2);
            userList.Add(seller3);

            _usersRepository.Setup(x => x.GetUserById(16).Result).Returns(buyer2);
            IUserService userService = new UserService(_usersRepository.Object, _usersMongoRepository.Object);

            //Act
            User testSubject = await userService.GetUserById(16);
            //Assert
            Assert.Equal(16, testSubject.Id);
        }

        [Fact]
        public async Task UpdateElectricCarTest()
        {
            //Arrange
            Mock<IUserRepository> _usersRepository = new Mock<IUserRepository>();
            Mock<IMongoDbRepository> _usersMongoRepository = new Mock<IMongoDbRepository>();

            List<User> userList = new List<User>();

            Buyer buyer = new Buyer
            {
                Id = 16,
                FirstName = "Edgar",
                LastName = "Juggernaut",
                Email = "Juggernaut@mail.com",
                PhoneNumber = "1568742135"
            };

            userList.Add(buyer);

            Buyer buyerUpdated = new Buyer
            {
                Id = 16,
                FirstName = "Edgar",
                LastName = "Rikuda",
                Email = "Rikuda@mail.com",
                PhoneNumber = "1564487135"
            };

            _usersRepository.Setup(x => x.UpdateUser(16, buyerUpdated));
            _usersRepository.Setup(x => x.GetUserById(16).Result).Returns(buyerUpdated);
            IUserService userService = new UserService(_usersRepository.Object, _usersMongoRepository.Object);

            //Act
            await userService.UpdateUser(16, buyerUpdated);
            var result = await userService.GetUserById(16);
            //Assert
            Assert.Equal("Rikuda", result.LastName);
        }
    }
}