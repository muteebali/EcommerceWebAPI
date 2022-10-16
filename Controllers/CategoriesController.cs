using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlinePOSAPI.FilterAttribute;
using OnlinePOSAPI.Models;
using OnlinePOSAPI.Models.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlinePOSAPI.Controllers
{
    [ApiController]
    [Route("ctg")]
    public class CategoriesController : ControllerBase
    {
        private readonly OnlinePOSContext _context;
        private readonly IMapper _mapper;
        public CategoriesController(OnlinePOSContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [Route("all")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategory()
        {
            return _mapper.Map<List<CategoryDTO>>(await _context.Category.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDTO>> GetCategory(int id)
        {
            var category = await _context.Category.FindAsync(id);
            if (category == null)
                return NotFound();

            return _mapper.Map<CategoryDTO>(category);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult<Category>> PostCategory(CategoryDTO categoryDto)
        {
            _context.Category.Add(_mapper.Map<Category> (categoryDto));
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategory", new { id = categoryDto.Id }, categoryDto);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<CategoryDTO>> PatchCategoryUpadte(int id,JsonPatchDocument jsonObj)
        {
            var category = await _context.Category.FindAsync(id);
            if (category == null)
                return NotFound();

            jsonObj.ApplyTo(category);
            await _context.SaveChangesAsync();

            return _mapper.Map<CategoryDTO>( category);
        }

    }
}
