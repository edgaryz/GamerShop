using Amazon.Runtime.Internal.Util;
using GamerShop.Core.Contracts;
using GamerShop.Core.Models;

namespace GamerShop.Core.Services
{
    public class UserService : IUserService
    {
        public readonly IUserRepository _userDbRepository;
        public readonly IMongoDbRepository _userMongoDbRepository;
        public UserService(IUserRepository userDbRepository, IMongoDbRepository userMongoDbRepository)
        {
            _userDbRepository = userDbRepository;
            _userMongoDbRepository = userMongoDbRepository;
        }

        public async Task<List<User>> GetAllUsers()
        {
            List<User> userList;

            if (await _userDbRepository.GetTotalUserCount() != await _userMongoDbRepository.GetUserCount())
            {
                await _userMongoDbRepository.ClearUserCache();
                userList = await _userDbRepository.GetAllUsers();
                await _userMongoDbRepository.InsertUserList(userList);
            }
            else
            {
                userList = await _userMongoDbRepository.GetAllUsers();
            }

            return userList;
        }

        public async Task<int> GetTotalUserCount()
        {
            return await _userDbRepository.GetTotalUserCount();
        }

        public async Task<User> GetUserById(int id)
        {
            var user = await _userMongoDbRepository.GetUserById(id);

            if (user != null)
            {
                return user;
            }

            user = await _userDbRepository.GetUserById(id);
            await _userMongoDbRepository.InsertUser(user);
            return user;
        }

        public async Task CreateUser(User user)
        {
            await _userDbRepository.CreateUser(user);
        }

        public async Task UpdateUser(int id, User updatedUser)
        {
            await _userDbRepository.UpdateUser(id, updatedUser);
        }

        public async Task DeleteUser(int id)
        {
            await _userDbRepository.DeleteUser(id);
        }
    }
}
