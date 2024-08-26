using GamerShop.Core.Contracts;
using GamerShop.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace GamerShop.API.Controllers
{
    public class OrderController : Controller
    {
        private readonly IGamerShopService _gamerShopService;
        public OrderController(IGamerShopService gamerShopService)
        {
            _gamerShopService = gamerShopService;
        }

        [HttpGet("GetAllOrders")]
        public async Task<IActionResult> GetAllOrders()
        {
            try
            {
                var allOrders = await _gamerShopService.GetAllOrders();
                return Ok(allOrders);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return NotFound();
            }
        }

        [HttpGet("GetOrderById")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            try
            {
                var Order = await _gamerShopService.GetOrderById(id);
                return Ok(Order);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return NotFound();
            }
        }

        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder([FromBody] Order order)
        {
            try
            {
                await _gamerShopService.CreateOrder(order);
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while creating a order: {ex.Message}");

                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                }
                return Problem();
            }
        }

        [HttpPatch("UpdateOrder/{id}")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] Order updatedOrder)
        {
            try
            {
                await _gamerShopService.UpdateOrder(id, updatedOrder);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Error while updating the order: ", ex.Message);
                return NotFound();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while updating the order: ", ex.Message);
                return Problem();
            }
        }

        [HttpDelete("DeleteOrder/{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            try
            {
                await _gamerShopService.DeleteOrder(id);
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while deleting the order: ", ex.Message);
                return NotFound();
            }
        }
    }
}
