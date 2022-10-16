using AutoMapper;
using EFCore.BulkExtensions;
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
    [Route("tag")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly OnlinePOSContext _context;
        private readonly IMapper _mapper;
        public TagsController(OnlinePOSContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<TagDTO>> GetTag()
        {
            return _mapper.Map<IEnumerable<TagDTO>>(await _context.Tag.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TagDTO>> GetTag(int id)
        {
            var tag = await _context.Tag.FindAsync(id);

            if (tag == null)
                return NotFound();

            return _mapper.Map<TagDTO>(tag);
        }


        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult<Tag>> PostTag(TagDTO[] tagDto)
        {
            if (tagDto.Count() > 0)
            {
                var tags = new List<Tag>();
                tagDto.ToList().ForEach(x => tags.Add(new Tag() { Title = x.Title.Trim() }));

                await _context.BulkInsertAsync(tags);

                return Ok();
            }
            else
                return new UnprocessableEntityObjectResult(ModelState);

        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<TagDTO>> PatchTag(int id, JsonPatchDocument jsonDoc)
        {
            var tag = await _context.Tag.FindAsync(id);
            if (tag == null)
                return NotFound();

            jsonDoc.ApplyTo(tag);
            await _context.SaveChangesAsync();
            return _mapper.Map<TagDTO>(tag);
        }

    }
}
