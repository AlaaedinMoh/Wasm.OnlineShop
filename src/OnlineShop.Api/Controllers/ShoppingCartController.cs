using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Api.Data;
using OnlineShop.Api.Entities;
using OnlineShop.Api.Extensions;
using OnlineShop.Shared.Dtos;

namespace OnlineShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly OnlineShopDbContext dbContext;

        public ShoppingCartController(OnlineShopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        [Route("{userId}/GetItems")]
        public async Task<ActionResult<IEnumerable<CartItemDto>>> GetItems(int userId)
        {
            try
            {
                if (dbContext.CartItems is null)
                    return NotFound();
                if (dbContext.CartItems.Any())
                    return NotFound();
                if(dbContext.Products is null)
                {
                    throw new Exception("No Products to show");
                }
                if (!dbContext.Products.Any())
                    throw new Exception("No Products to show");
                IEnumerable<CartItemDto> cartItemsDto = dbContext.CartItems.ConvertToDto(dbContext.Products);
                return Ok(cartItemsDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CartItemDto>> GetCartItem(int id)
        {
            try
            {
                CartItem? cartItem = await dbContext.CartItems.FindAsync(id);
                if (cartItem is null)
                    return NotFound();
                Product? product = await dbContext.Products.FindAsync(cartItem.ProductId);
                if (product is null)
                    return NotFound();
                CartItemDto cartItemDto = cartItem.ConvertToDto(product);
                return Ok(cartItemDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
