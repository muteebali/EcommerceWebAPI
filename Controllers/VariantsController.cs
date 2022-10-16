using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlinePOSAPI.Models;
using OnlinePOSAPI.Models.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlinePOSAPI.Controllers
{
    [Route("var")]
    [ApiController]
    public class VariantsController : ControllerBase
    {
        private readonly OnlinePOSContext _context;
        private readonly IMapper _mapper;
        public VariantsController(OnlinePOSContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [Route("all")]
        [HttpGet]
        public async Task<IEnumerable<VariantDTO>> GetVariant()
        {
            return _mapper.Map<IEnumerable<VariantDTO>>(await _context.Variant.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VariantDTO>> GetVariant(byte id)
        {
            var variant = _mapper.Map<VariantDTO>(await _context.Variant.FindAsync(id));

            if (variant == null)
                return NotFound();

            return variant;
        }


        [HttpPost]
        public async Task<ActionResult<Variant>> PostVariant(VariantDTO variantDto)
        {
            var variant = _mapper.Map<VariantDTO, Variant>(variantDto);
            _context.Variant.Add(variant);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVariant", new { id = variant.Id }, variantDto);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<Variant>> PatchVariant(byte id,[FromBody] JsonPatchDocument patchDocument)
        {
            var variant = await _context.Variant.FindAsync(id);
            if (variant == null)
                return NotFound();

            variant.ModifiedOn = DateTime.UtcNow;
            patchDocument.ApplyTo(variant);
            await _context.SaveChangesAsync();
            return Ok(variant);
        }

    }
}
