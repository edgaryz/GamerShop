using GamerShop.Core.Contracts;
using GamerShop.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace GamerShop.Core.Repositories
{
    public class UserRepository : IUserRepository
    {
        public async Task<List<User>> GetAllUsers()
        {
            using (var context = new MyDbContext())
            {
                List<User> allUsers = await context.Users.ToListAsync();
                return allUsers;
            }
        }

        public async Task<int> GetTotalUserCount()
        {
            using (var context = new MyDbContext())
            {
                return await context.Users.CountAsync();
            }
        }

        public async Task<User> GetUserById(int id)
        {
            using (var context = new MyDbContext())
            {
                return await context.Users.FindAsync(id);
            }
        }

        public async Task CreateUser(User user)
        {
            using (var context = new MyDbContext())
            {
                //AddAsync does not work, so we use Update
                context.Users.Update(user);
                await context.SaveChangesAsync();
            }
        }

        public async Task UpdateUser(int id, User updatedUser)
        {
            using (var context = new MyDbContext())
            {
                var existingUser = await context.Users.FindAsync(id);
                if (existingUser != null)
                {
                    existingUser.FirstName = updatedUser.FirstName;
                    existingUser.LastName = updatedUser.LastName;
                    existingUser.Email = updatedUser.Email;
                    existingUser.PhoneNumber = updatedUser.PhoneNumber;

                    await context.SaveChangesAsync();
                }
                else
                {
                    throw new ArgumentException("User not found");
                }
            }
        }

        public async Task DeleteUser(int id)
        {
            using (var context = new MyDbContext())
            {
                context.Users.Remove(await context.Users.FindAsync(id));
                await context.SaveChangesAsync();
            }
        }
    }
}
