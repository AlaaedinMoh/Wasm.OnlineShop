using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Api.Data;
using OnlineShop.Api.Entities;
using OnlineShop.Api.Extensions;
using OnlineShop.Shared.Dtos;

namespace OnlineShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly OnlineShopDbContext dbContext;

        public ProductController(OnlineShopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetItems()
        {
            try
            {
                if(dbContext.Products is null || dbContext.ProductCategories is null)
                    return NotFound();
                var productDtos = dbContext.Products.ConvertToDto(dbContext.ProductCategories);
                return Ok(productDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving products data from database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDto>> GetItem(int id)
        {
            try
            {
                if (dbContext.Products is null || dbContext.ProductCategories is null)
                    return NotFound();
                Product? product = await dbContext.Products.FindAsync(id);
                if (product is null)
                    return BadRequest();
                ProductCategory? category = await dbContext.ProductCategories.FindAsync(product.CategoryId);
                if (category is null)
                    return BadRequest();
                return product.ConvertDto(category);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving products data from database");
            }
        }
    }
}
