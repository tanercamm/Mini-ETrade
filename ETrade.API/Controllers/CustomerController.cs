using ETrade.Application.DTOs.Customer;
using ETrade.Application.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace ETrade.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET: api/customer
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _customerService.GetAll();
            return Ok(customers);
        }

        // GET: api/customer/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var customer = await _customerService.GetByIdAsync(id);
            if (customer == null)
                return NotFound("Customer not found.");

            return Ok(customer);
        }

        // POST: api/customer
        [HttpPost]
        public async Task<IActionResult> Create(CreateCustomerDTO customerDto)
        {
            if (customerDto == null)
                return BadRequest("Invalid customer data.");

            await _customerService.AddAsync(customerDto);
            return CreatedAtAction(nameof(GetById), new { id = customerDto.Name }, customerDto);
        }

        // PUT: api/customer/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, UpdateCustomerDTO customerDto)
        {
            if (customerDto == null || id != customerDto.Id.ToString())
                return BadRequest("Invalid request data.");

            await _customerService.UpdateAsync(customerDto);
            return NoContent();
        }

        // DELETE: api/customer/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _customerService.DeleteAsync(id);
            return NoContent();
        }
    }
}
