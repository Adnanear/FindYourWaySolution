using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FindYourWay.Data;
using FindYourWay.Models;
using FindYourWay.Models.Dto;
using Microsoft.AspNetCore.Authorization;

namespace FindYourWay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrdersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await _context.Orders.ToListAsync();
        }

        // GET: api/Orders/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order is null) return NotFound();

            return order;
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutOrder(int id, OrderDto order)
        {
            var targetOrder = await _context.Orders.FindAsync(id);
            var buyer = await _context.Accounts.FindAsync(order.ClientId);
            var product = await _context.Services.FindAsync(order.ServiceId);
            if(targetOrder is null || buyer is null || product is null) return NotFound();

            targetOrder.ClientId = order.ClientId;
            targetOrder.ServiceId = order.ServiceId;
            targetOrder.Status = (Enums.OrderStatus)order.Status;
            targetOrder.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(OrderDto order)
        {
            var buyer = await _context.Accounts.FindAsync(order.ClientId);
            var product = await _context.Services.FindAsync(order.ServiceId);
            if (buyer is null || product is null) return NotFound();

            Order newOrder = new()
            {
                ClientId = order.ClientId,
                ServiceId = order.ServiceId,
                Status = (Enums.OrderStatus)order.Status,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Orders.Add(newOrder);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new { id = newOrder.Id }, newOrder);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order is null) return NotFound();
            

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
