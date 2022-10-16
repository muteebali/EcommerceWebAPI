using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlinePOSAPI.FilterAttribute;
using OnlinePOSAPI.Models;
using OnlinePOSAPI.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlinePOSAPI.Controllers
{
    [Route("order")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OnlinePOSContext _context;
        private readonly IMapper _mapper;

        public OrdersController(OnlinePOSContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<OrderDTO>> GetOrder()
        {
            return _mapper.Map<IEnumerable<OrderDTO>>(await _context.Order.ToListAsync());
        }

        [HttpGet("details/{id:int:min(1)}")]
        public async Task<ActionResult<OrderDetailsDTO>> GetOrderDetails(int id)
        {
            var order = await _context.Order.FindAsync(id);
            if (order is null)
                return NotFound();

            var orderDetails = new OrderDetailsDTO()
            {
                OrderLineItems = _mapper.Map<List<OrderLineItemDTO>>(await _context.OrderLineItem.Where(x => x.OrderId == id).ToListAsync()),
                Order = _mapper.Map<OrderDTO>(order)
            };

            return orderDetails;
        }

        [HttpGet("{id:int:min(1)}")]
        [HttpGet("{id:int:min(1)}")]
        public async Task<ActionResult<OrderDTO>> GetOrder(int id)
        {
            var order = _mapper.Map<OrderDTO>(await _context.Order.FindAsync(id));

            if (order == null)
                return NotFound();

            return order;
        }
        [Route("orderitems")]
        [HttpDelete()]
        public async Task<ActionResult> DeleteOrderLineItems([FromBody] int[] ids)
        {
            var todoItem = await _context.OrderLineItem.Where(x => ids.Contains(x.Id)).ToListAsync();
            if (todoItem == null || todoItem.Count == 0)
                return NotFound();
            _context.OrderLineItem.RemoveRange(todoItem);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult> PutOrder(int id, Order order)
        {
            if (id != order.Id)
                return BadRequest();

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult<Order>> PostOrder(OrderDetailsDTO orderDetails)
        {

            using var transactionContext = _context.Database.BeginTransaction();
            try
            {
                //Adding order details
                var orderDetail = _mapper.Map<Order>(orderDetails.Order);
                _context.Order.Add(orderDetail);
                await _context.SaveChangesAsync();

                //Adding orderline item and link them to order id
                var orderLineItems = _mapper.Map<List<OrderLineItem>>(orderDetails.OrderLineItems);
                orderLineItems.ForEach(x => x.OrderId = orderDetail.Id);
                _context.OrderLineItem.AddRange(orderLineItems);
                await _context.SaveChangesAsync();

                await transactionContext.CommitAsync();
                return CreatedAtAction("GetOrder", new { id = orderDetail.Id }, orderDetails);

            }
            catch (Exception ex)
            {
                await transactionContext.RollbackAsync();
                return Content(ex.InnerException.ToString());
            }

        }
        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateOrderStatus(int id, [FromBody] JsonPatchDocument jsonObj)
        {
            var order = await _context.Order.FindAsync(id);
            if (order == null)
                return NotFound();
            order.ModifiedOn = DateTime.UtcNow;
            jsonObj.ApplyTo(order);
            await _context.SaveChangesAsync();
            return Ok();
        }

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.Id == id);
        }
    }
}
