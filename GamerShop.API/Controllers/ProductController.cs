using GamerShop.Core.Contracts;
using GamerShop.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace GamerShop.API.Controllers
{
    public class ProductController : Controller
    {
        private readonly IBusinessLogicService _businessLogicService;
        public ProductController(IBusinessLogicService businessLogicService)
        {
            _businessLogicService = businessLogicService;
        }

        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var allProducts = await _businessLogicService.GetAllProducts();
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
                var Product = await _businessLogicService.GetProductById(id);
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
                await _businessLogicService.CreateProduct(product);
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
                await _businessLogicService.UpdateProduct(id, updatedProduct);
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
                await _businessLogicService.DeleteProduct(id);
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
