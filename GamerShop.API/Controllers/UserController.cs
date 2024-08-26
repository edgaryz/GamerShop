using GamerShop.Core.Contracts;
using GamerShop.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace GamerShop.API.Controllers
{
    public class UserController : Controller
    {
        private readonly IGamerShopService _gamerShopService;
        public UserController(IGamerShopService gamerShopService)
        {
            _gamerShopService = gamerShopService;
        }

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var allUsers = await _gamerShopService.GetAllUsers();
                return Ok(allUsers);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return NotFound();
            }
        }

        [HttpGet("GetUserById")]
        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                var user = await _gamerShopService.GetUserById(id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return NotFound();
            }
        }

        [HttpPost("CreateUser/{userType}")]
        public async Task<IActionResult> CreateUser([FromBody] User user, string userType)
        {
            User userByType;

            if (userType == "Buyer")
            {
                userByType = new Buyer()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber
                };
            }
            else if (userType == "Seller")
            {
                userByType = new Seller()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber
                };
            }
            else
            {
                throw new Exception("no such type");
            }

            try
            {
                await _gamerShopService.CreateUser(userByType);
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while creating a user: {ex.Message}");

                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                }
                return Problem();
            }
        }

        [HttpPatch("UpdateUser/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User updatedUser)
        {
            try
            {
                await _gamerShopService.UpdateUser(id, updatedUser);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Error while updating the user: ", ex.Message);
                return NotFound();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while updating the user: ", ex.Message);
                return Problem();
            }
        }

        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                await _gamerShopService.DeleteUser(id);
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while deleting the user: ", ex.Message);
                return NotFound();
            }
        }
    }
}
