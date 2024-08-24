using GamerShop.Core.Models;
using GamerShop.Core.Repositories;

namespace GamerShop.Core.Contracts
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsers();
        Task<int> GetTotalUserCount();
        Task<User> GetUserById(int id);
        Task CreateUser(User user);
        Task UpdateUser(int id, User updatedUser);
        Task DeleteUser(int id);
    }
}
