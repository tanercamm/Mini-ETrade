using ETrade.Application.DTOs.Product;
using ETrade.Application.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETrade.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/product
        [Authorize(Roles ="Admin,Customer")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAll();
            if (products == null)
                return NotFound("Products not found.");
            return Ok(products);
        }

        // GET: api/product/{id}
        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
                return NotFound("Product not found.");

            return Ok(product);
        }

        // POST: api/product
        [Authorize("Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductDTO productDto)
        {
            if (productDto == null)
                return BadRequest("Invalid product data.");

            await _productService.AddAsync(productDto);
            return CreatedAtAction(nameof(GetById), new { id = productDto.Name }, productDto);
        }

        // PUT: api/product/{id}
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, UpdateProductDTO productDto)
        {
            if (productDto == null)
                return BadRequest("Invalid request data.");

            await _productService.UpdateAsync(id, productDto);
            return NoContent();
        }

        // DELETE: api/product/{id}
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _productService.DeleteAsync(id);
            return NoContent();
        }
    }
}
