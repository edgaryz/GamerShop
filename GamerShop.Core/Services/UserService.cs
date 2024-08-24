using GamerShop.Core.Contracts;
using GamerShop.Core.Models;

namespace GamerShop.Core.Services
{
    public class UserService : IUserService
    {
        public readonly IUserRepository _userDbRepository;
        public UserService(IUserRepository userDbRepository)
        {
            _userDbRepository = userDbRepository;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _userDbRepository.GetAllUsers();
        }

        public async Task<int> GetTotalUsersCount()
        {
            return await _userDbRepository.GetTotalUserCount();
        }

        public async Task<User> GetUsersById(int id)
        {
            return await _userDbRepository.GetUserById(id);
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
