using GamerShop.Core.Models;

namespace GamerShop.Core.Contracts
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsers();
        Task<int> GetTotalUsersCount();
        Task<User> GetUsersById(int id);
        Task CreateUser(User user);
        Task UpdateUser(int id, User updatedUser);
        Task DeleteUser(int id);
    }
}
