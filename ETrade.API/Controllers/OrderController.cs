﻿using ETrade.Application.DTOs.Order;
using ETrade.Application.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETrade.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // GET: api/order
        [Authorize(Roles = "Admin,Customer")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _orderService.GetAll();
            if (orders == null)
                return NotFound("Orders not found.");
            return Ok(orders);
        }

        // GET: api/order/{id}
        [Authorize(Roles="Admin,Customer")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var order = await _orderService.GetByIdAsync(id);
            if (order == null)
                return NotFound("Order not found.");

            return Ok(order);
        }

        // POST: api/order
        [Authorize(Roles = "Customer")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderDTO orderDto)
        {
            if (orderDto == null)
                return BadRequest("Invalid order data.");

            await _orderService.AddAsync(orderDto);
            return CreatedAtAction(nameof(GetById), new { id = orderDto.Description }, orderDto);
        }

        // PUT: api/order/{id}
        [Authorize(Roles = "Customer")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, UpdateOrderDTO orderDto)
        {
            if (orderDto == null)
                return BadRequest("Invalid request data.");

            await _orderService.UpdateAsync(id, orderDto);
            return NoContent();
        }

        // DELETE: api/order/{id}
        [Authorize(Roles = "Admin,Customer")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _orderService.DeleteAsync(id);
            return NoContent();
        }
    }
}
