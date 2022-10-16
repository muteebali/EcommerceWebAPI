using AutoMapper;
using EFCore.BulkExtensions;
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
    [Route("ProductDetail")]
    [ApiController]
    public class ProductDetailController : ControllerBase
    {
        private readonly OnlinePOSContext _context;
        private readonly IMapper _mapper;

        public ProductDetailController(OnlinePOSContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<IEnumerable<ProductDetailDTO>>> GetProductVariants()
        {
            return _mapper.Map<List<ProductDetailDTO>>(await _context.ProductVariant.ToListAsync());
        }

        [HttpGet("{id:int:min(1)}")]
        public async Task<ActionResult<ProductDetailDTO>> GetProductVariants(int id)
        {
            var productVariants = await _context.ProductVariant.FindAsync(id);
            if (productVariants == null)
                return NotFound();
            return _mapper.Map<ProductDetailDTO>(productVariants);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult<ProductDetailDTO>> PostProductDetails(ProductDetailDTO productDetialDto)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var productVariant = _mapper.Map<ProductDetail>(productDetialDto);
                _context.ProductDetail.Add(productVariant);
                await _context.SaveChangesAsync();

                var variants = new List<ProductVariant>();
                productDetialDto.ProductVariantIDs?.ForEach(x => variants.Add(new ProductVariant
                {
                    ProductDetailId = productVariant.Id, //Product variant  id
                    VariantValueId = x  // variant value id
                }));

                await _context.BulkInsertAsync(variants);
                await transaction.CommitAsync();
                return Ok("Transaction Successfull");
            }
            catch(Exception ex)
            {
                await transaction.RollbackAsync();
                return BadRequest("Transaction has been rollbacked");
            }
        }

        [HttpPatch("partialUpdate/{id:int:min(1)}")]
        public async Task<ActionResult<ProductVariant>> PatchProductVariantUpdate(int id, JsonPatchDocument jsonObj)
        {
            var productVariants = await _context.ProductVariant.FindAsync(id);
            if (productVariants == null)
                return NotFound();

            jsonObj.ApplyTo(productVariants);
            await _context.SaveChangesAsync();
            return productVariants;
        }

    }
}
