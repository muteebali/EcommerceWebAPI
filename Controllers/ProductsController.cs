using AutoMapper;
using EFCore.BulkExtensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlinePOSAPI.FilterAttribute;
using OnlinePOSAPI.Models;
using OnlinePOSAPI.Models.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OnlinePOSAPI.Controllers
{
    [Route("Product")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly OnlinePOSContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;

        public ProductsController(OnlinePOSContext context, IMapper mapper, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _environment = env;
        }

        [HttpGet]
        public async Task<JsonResult> GetProducts()
        {
            return new JsonResult(await _context.Product
                            .OrderByDescending(x => x.CreatedOn).Skip(0).Take(10)
                             .Include(x => x.ProductDetail)
                             .ThenInclude(x => x.ProductMedia)
                             .Include(x => x.Category)
                             .Include(x => x.Promotion)

                             .Select(y => new
                             {
                                 y.Id,
                                 y.CreatedOn,
                                 y.Title,
                                 Category = y.Category.Title,
                                 Discount = y.Promotion,
                                 y.Visible,
                                 y.ProductDetail.BasePrice,
                                 y.ProductDetail.RetailPrice,
                                 thumbnail = y.ProductDetail.ProductMedia.Where(x=>x.Thumbnail)
                                                             .Select(z => new
                                                             {
                                                                 z.Id,
                                                                 Url = string.Concat(z.Url, z.Extension)
                                                             })
                             }).ToListAsync()
                             );
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetProduct(int id)
        {
            var product = await _context.Product.FindAsync(id);
            if (product == null)
                return NotFound();

            return new JsonResult(await
                          _context.Product.Where(x => x.Id == id)
                          .Include(product => product.Category)
                          .Include(product => product.Promotion)
                          .Include(product => product.ProductDetail)
                          .ThenInclude(prodDetail => prodDetail.ProductMedia)
                          .Include(product => product.ProductDetail.ProductVariant)
                          .ThenInclude(productVariant => productVariant.VariantValue)
                          .Select(x => new
                          {
                              x.Id,
                              x.Brand,
                              x.Visible,
                              x.CreatedOn,
                              x.ModifiedOn,
                              x.ProductDetail.RetailPrice,
                              x.ProductDetail.BasePrice,
                              Description = x.ProductDetail.LongDesc,
                              Category = x.Category.Title,
                              Promotion = x.Promotion,
                              Media = x.ProductDetail.ProductMedia,
                              Variants = x.ProductDetail.ProductVariant
                              .Select(y => new
                              {
                                  Variant = y.VariantValue.Variant.Attribute,
                                  VariantValue = y.VariantValue.AttributeValue,
                                  y.Sku,
                                  y.Stock,
                                  y.Id
                              }).ToList()
                          }).ToListAsync());
        }


        [HttpPut("{id:int:min(1)}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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


        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPost]
        public async Task<ActionResult<ProductDataDTO>> PostProduct([FromBody] ProductDataDTO productDetails)
        {
            using var transactionContext = _context.Database.BeginTransaction();

            try
            {
                #region Saving Produc Basic Info
                var product = _mapper.Map<Product>(productDetails.BasicInfo);
                _context.Product.Add(product);
                await _context.SaveChangesAsync();
                #endregion

                #region Product Detail
                productDetails.ProductDetail.ProductId = product.Id;
                var productDetail = _mapper.Map<ProductDetail>(productDetails.ProductDetail);
                _context.ProductDetail.Add(productDetail);
                await _context.SaveChangesAsync();

                var variants = new List<ProductVariant>();
                productDetails.ProductDetail.ProductVariantIDs?.ForEach(variant => variants.Add(new ProductVariant
                {
                    ProductDetailId = productDetail.Id, //Product variant  id
                    VariantValueId = variant, // variant value id
                    Sku = string.Format("{0}-{1}-{2}-{3}",
                                            product.Id,
                                            product.CategoryId,
                                            productDetail.Id,
                                            variant)
                }));
                await _context.BulkInsertAsync(variants);
                #endregion

                #region Tag insertion

                //Get id of currently inserted product
                var insertedId = product.Id;

                if (productDetails.Tags.Count() > 0)
                {
                    var tags = new List<ProductTags>();
                    productDetails.Tags.ForEach(tag => tags.Add(new ProductTags
                    {
                        ProductId = insertedId,
                        TagId = tag
                    }));
                    await _context.BulkInsertAsync(tags);
                }
                #endregion

                await transactionContext.CommitAsync();
                return CreatedAtAction("GetProduct", new { id = productDetails.BasicInfo.Id }, productDetails);
            }
            catch (Exception ex)
            {
                await transactionContext.RollbackAsync();
                return NotFound();
            }
        }

        [HttpPost("upload/{variantID:int:min(1)}")]
        public async Task<ActionResult<ProductMedia>> UploadMedia(int productDetailID)
        {

            if (Request.Form.Files.Count > 0)
            {
                var mediaMeta = new List<ProductMeta>();
                var mediaDetails = new List<ProductMedia>();

                #region Iterating files from http request body
                foreach (var file in Request.Form.Files)
                {
                    if (file.Length == 0 || file.Name is null)
                        return Content("File is not selected");

                    #region Folder and file managment
                    FileInfo fileInfo = new FileInfo(file.FileName);
                    var folderPath = Path.Combine("" + _environment.ContentRootPath + "\\Resources\\Images\\");
                    if (!Directory.Exists(folderPath))
                        Directory.CreateDirectory(folderPath);

                    var path = Path.Combine(folderPath, DateTime.Now.Ticks.ToString());
                    #endregion

                    #region Saving file locally on server
                    using (FileStream stream = new FileStream(path + fileInfo.Extension,
                                                              FileMode.Create, FileAccess.Write))
                    {
                        await file.CopyToAsync(stream);
                    }
                    #endregion

                    #region   For Saving only meta of image without blob
                    mediaDetails.Add(new ProductMedia
                    {
                        ProductDetailId = productDetailID, //FK Product id
                        Extension = fileInfo.Extension, //Extension of file
                        Size = file.Length, //Size of file
                        Url = path, //path of saving directory,
                        Thumbnail = file.Name == "thumbnail"
                    });

                    //add blob data in DB
                    using MemoryStream ms = new MemoryStream();
                    await file.CopyToAsync(ms);

                    #endregion

                    #region For Blob image saving in DB
                    mediaMeta.Add(new ProductMeta()
                    {
                        ProductDetailId = productDetailID,
                        ImgBlob = ms.ToArray(),
                    });
                    #endregion
                }
                #endregion

                #region Bulk Insertion in DB 
                //Saving Media files urls and extension in database
                if (mediaMeta.Count > 0)
                    await _context.BulkInsertAsync(mediaMeta);

                //Saving Media files urls and extension in database
                if (mediaDetails.Count > 0)
                    await _context.BulkInsertAsync(mediaDetails);
                #endregion

                return Ok();
            }
            else
                return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Product.Remove(product);
            await _context.SaveChangesAsync();

            return product;
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}
