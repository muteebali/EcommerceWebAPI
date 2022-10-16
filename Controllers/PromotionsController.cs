using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlinePOSAPI.Models;
using OnlinePOSAPI.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlinePOSAPI.Controllers
{
    [Route("Promotion")]
    [ApiController]
    public class PromotionsController : ControllerBase
    {
        private readonly OnlinePOSContext _context;
        private readonly IMapper _mapper;

        public PromotionsController(OnlinePOSContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PromotionDTO>>> GetPromotion()
        {
            return _mapper.Map<List<PromotionDTO>>(await _context.Promotion.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Promotion>> GetPromotion(int id)
        {
            var promotion = await _context.Promotion.FindAsync(id);

            if (promotion == null)
            {
                return NotFound();
            }

            return promotion;
        }
        [HttpPatch("{id}")]
        public async Task<ActionResult<Variant>> PatchVariant(int id, [FromBody] JsonPatchDocument patchDocument)
        {
            var variant = await _context.Promotion.FindAsync(id);
            if (variant == null)
                return NotFound();

            variant.ModifiedOn = DateTime.UtcNow;
            patchDocument.ApplyTo(variant);
            await _context.SaveChangesAsync();
            return Ok(variant);
        }


        [HttpPost]
        public async Task<ActionResult<Promotion>> PostPromotion(PromotionDTO promotionDto)
        {
            _context.Promotion.Add(_mapper.Map<Promotion>(promotionDto));
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPromotion", new { id = promotionDto.Id }, promotionDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<PromotionDTO>> DeletePromotion(int id)
        {
            var promotion = await _context.Promotion.FindAsync(id);
            if (promotion == null)
                return NotFound();

            _context.Promotion.Remove(promotion);
            await _context.SaveChangesAsync();

            return _mapper.Map<PromotionDTO>(promotion);
        }

        
    }
}
