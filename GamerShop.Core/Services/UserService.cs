﻿using GamerShop.Core.Contracts;
using GamerShop.Core.Models;
using Serilog;

namespace GamerShop.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userDbRepository;
        private readonly IMongoDbRepository _userMongoDbRepository;
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
                Log.Information("Got Users form DB");
            }
            else
            {
                Log.Information("Got Users form Mongo DB");
                userList = await _userMongoDbRepository.GetAllUsers();
            }

            return userList;
        }

        public async Task<int> GetTotalUserCount()
        {
            Log.Information("GetTotalUserCount called");
            return await _userDbRepository.GetTotalUserCount();
        }

        public async Task<User> GetUserById(int id)
        {
            var user = await _userMongoDbRepository.GetUserById(id);

            if (user != null)
            {
                Log.Information($"User with id {user.Id} found in Mongo DB");
                return user;
            }

            user = await _userDbRepository.GetUserById(id);
            await _userMongoDbRepository.InsertUser(user);
            Log.Information($"User with id {user.Id} found in DB");
            return user;
        }

        public async Task CreateUser(User user)
        {
            Log.Information("CreateUser called");
            await _userDbRepository.CreateUser(user);
        }

        public async Task UpdateUser(int id, User updatedUser)
        {
            Log.Information("UpdateUser called");
            await _userDbRepository.UpdateUser(id, updatedUser);
        }

        public async Task DeleteUser(int id)
        {
            Log.Information("DeleteUser called");
            await _userDbRepository.DeleteUser(id);
        }
    }
}
