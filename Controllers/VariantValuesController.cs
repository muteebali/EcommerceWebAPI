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
    [Route("var-attr")]
    [ApiController]
    public class VariantValuesController : ControllerBase
    {
        private readonly OnlinePOSContext _context;
        private readonly IMapper _mapper;

        public VariantValuesController(OnlinePOSContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<VariantValueDTO>> GetVariantValue()
        {
            return _mapper.Map<IEnumerable<VariantValueDTO>>(await _context.VariantValue.ToListAsync());
        }

        // GET: api/VariantValues/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VariantValueDTO>> GetVariantValue(byte id)
        {
            var variantValue = await _context.VariantValue.FindAsync(id);

            if (variantValue == null)
            {
                return NotFound();
            }

            return _mapper.Map<VariantValueDTO>( variantValue);
        }

       

        [HttpPost]
        public async Task<ActionResult<VariantValue>> PostVariantValue(VariantValueDTO variantValueDto)
        {
            _context.VariantValue.Add(_mapper.Map<VariantValue>(variantValueDto));
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVariantValue", new { id = variantValueDto.Id }, variantValueDto);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<VariantValue>> PatchVariantValue(byte id,[FromBody] JsonPatchDocument jsonObj)
        {
            var variantValue = await _context.VariantValue.FindAsync(id);
            if (variantValue == null)
                return NotFound();

            variantValue.ModifiedOn = DateTime.UtcNow;
            jsonObj.ApplyTo(variantValue);
            await _context.SaveChangesAsync();

            return Ok(_mapper.Map<VariantValueDTO>( variantValue));
        }

       
    }
}
