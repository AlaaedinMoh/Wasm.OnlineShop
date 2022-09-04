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
                if (dbContext.Products is null)
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

        [HttpPost]
        public async Task<ActionResult<CartItemDto>> AddItem([FromBody] CartItemToAddDto cartItemToAdd)
        {
            try
            {
                CartItem? newItem = await DoAddCartItemAsync(cartItemToAdd);
                if (newItem is null)
                    return NoContent();
                Product product = dbContext.Products.Find(newItem.ProductId);
                if (product is null)
                    throw new Exception($"Somwthing went wrong when attempting tp retrieve product (productId:({cartItemToAdd.ProductId}))");
                CartItemDto newItemDto = newItem.ConvertToDto(product);
                return CreatedAtAction(nameof(GetCartItem), new { id = newItemDto.Id }, newItemDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        private async Task<bool> CartItemExists(int cartId, int productId)
        {
            return await this.dbContext.CartItems.AnyAsync(c => c.CartId == cartId &&
                                                                     c.ProductId == productId);
        }

        private async Task<CartItem> DoAddCartItemAsync(CartItemToAddDto cartItemToAdd)
        {
            if (!await CartItemExists(cartItemToAdd.CartId, cartItemToAdd.ProductId))
            {
                var item = await (from product in this.dbContext.Products
                                  where product.Id == cartItemToAdd.ProductId
                                  select new CartItem
                                  {
                                      CartId = cartItemToAdd.CartId,
                                      ProductId = product.Id,
                                      Quantity = cartItemToAdd.Quantity
                                  }).SingleOrDefaultAsync();
                if (item is not null)
                {
                    var result = await dbContext.CartItems.AddAsync(item);
                    await this.dbContext.SaveChangesAsync();
                    return result.Entity;
                }
            }
            return null;
        }
    }
}
