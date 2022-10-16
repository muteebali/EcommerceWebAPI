using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlinePOSAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlinePOSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemsController : ControllerBase
    {
        private readonly OnlinePOSContext _context;

        public OrderItemsController(OnlinePOSContext context)
        {
            _context = context;
        }

        // GET: api/OrderItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderLineItem >>> GetOrderItem()
        {
            return await _context.OrderLineItem.ToListAsync();
        }

        // GET: api/OrderItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderLineItem>> GetOrderItem(int id)
        {
            var orderItem = await _context.OrderLineItem.FindAsync(id);

            if (orderItem == null)
            {
                return NotFound();
            }

            return orderItem;
        }

        // PUT: api/OrderItems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderItem(int id, OrderLineItem orderItem)
        {
            if (id != orderItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(orderItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/OrderItems
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<OrderLineItem>> PostOrderItem(OrderLineItem orderItem)
        {
            _context.OrderLineItem.Add(orderItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderItem", new { id = orderItem.Id }, orderItem);
        }

        // DELETE: api/OrderItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<OrderLineItem>> DeleteOrderItem(int id)
        {
            var orderItem = await _context.OrderLineItem.FindAsync(id);
            if (orderItem == null)
            {
                return NotFound();
            }

            _context.OrderLineItem.Remove(orderItem);
            await _context.SaveChangesAsync();

            return orderItem;
        }

        private bool OrderItemExists(int id)
        {
            return _context.OrderLineItem.Any(e => e.Id == id);
        }
    }
}
