using GamerShop.Core.Contracts;
using GamerShop.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace GamerShop.API.Controllers
{
    public class ProductController : Controller
    {
        private readonly IGamerShopService _gamerShopService;
        public ProductController(IGamerShopService gamerShopService)
        {
            _gamerShopService = gamerShopService;
        }

        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var allProducts = await _gamerShopService.GetAllProducts();
                return Ok(allProducts);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return NotFound();
            }
        }

        [HttpGet("GetProductById")]
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                var Product = await _gamerShopService.GetProductById(id);
                return Ok(Product);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return NotFound();
            }
        }

        [HttpPost("CreateProduct")]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            try
            {
                await _gamerShopService.CreateProduct(product);
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while creating a product: {ex.Message}");

                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                }
                return Problem();
            }
        }

        [HttpPatch("UpdateProduct/{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product updatedProduct)
        {
            try
            {
                await _gamerShopService.UpdateProduct(id, updatedProduct);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Error while updating the product: ", ex.Message);
                return NotFound();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while updating the product: ", ex.Message);
                return Problem();
            }
        }

        [HttpDelete("DeleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                await _gamerShopService.DeleteProduct(id);
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while deleting the product: ", ex.Message);
                return NotFound();
            }
        }
    }
}
