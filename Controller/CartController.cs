using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EcommerceApi.Data;
using EcommerceApi.Models;
using EcommerceApi.DTOs;

namespace EcommerceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Cart/user/
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<CartDto>>> GetCartByUser(int userId)
        {
            return await _context.CartItems
                .Include(c => c.Product)
                .Where(c => c.UserId == userId)
                .Select(c => new CartDto
                {
                    Id = c.Id,
                    UserId = c.UserId,
                    ProductId = c.ProductId,
                    ProductName = c.Product.Name,
                    ProductPrice = c.Product.Price,
                    ProductImageUrl = c.Product.ImageUrl,
                    Quantity = c.Quantity,
                    AddedAt = c.AddedAt
                })
                .ToListAsync();
        }

        // POST: api/Cart
        [HttpPost]
        public async Task<ActionResult<CartDto>> AddToCart(CreateCartDto createCartDto)
        {
          
            var user = await _context.Users.FindAsync(createCartDto.UserId);
            if (user == null)
            {
                return BadRequest("User not found");
            }

        
            var product = await _context.Products.FindAsync(createCartDto.ProductId);
            if (product == null)
            {
                return BadRequest("Product not found");
            }

        
            var existingCartItem = await _context.CartItems
                .FirstOrDefaultAsync(c => c.UserId == createCartDto.UserId && c.ProductId == createCartDto.ProductId);

            if (existingCartItem != null)
            {
        
                existingCartItem.Quantity += createCartDto.Quantity;
                await _context.SaveChangesAsync();

                return Ok(new CartDto
                {
                    Id = existingCartItem.Id,
                    UserId = existingCartItem.UserId,
                    ProductId = existingCartItem.ProductId,
                    ProductName = product.Name,
                    ProductPrice = product.Price,
                    ProductImageUrl = product.ImageUrl,
                    Quantity = existingCartItem.Quantity,
                    AddedAt = existingCartItem.AddedAt
                });
            }

      
            var cartItem = new Cart
            {
                UserId = createCartDto.UserId,
                ProductId = createCartDto.ProductId,
                Quantity = createCartDto.Quantity,
                AddedAt = DateTime.UtcNow
            };

            _context.CartItems.Add(cartItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCartByUser), new { userId = cartItem.UserId }, new CartDto
            {
                Id = cartItem.Id,
                UserId = cartItem.UserId,
                ProductId = cartItem.ProductId,
                ProductName = product.Name,
                ProductPrice = product.Price,
                ProductImageUrl = product.ImageUrl,
                Quantity = cartItem.Quantity,
                AddedAt = cartItem.AddedAt
            });
        }

        // PUT: api/Cart/
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCartItem(int id, UpdateCartDto updateCartDto)
        {
            var cartItem = await _context.CartItems.FindAsync(id);
            if (cartItem == null)
            {
                return NotFound();
            }

            cartItem.Quantity = updateCartDto.Quantity;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CartItemExists(id))
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

        // DELETE: api/Cart/
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCartItem(int id)
        {
            var cartItem = await _context.CartItems.FindAsync(id);
            if (cartItem == null)
            {
                return NotFound();
            }

            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Cart/user/
        [HttpDelete("user/{userId}")]
        public async Task<IActionResult> ClearCart(int userId)
        {
            var cartItems = await _context.CartItems
                .Where(c => c.UserId == userId)
                .ToListAsync();

            if (!cartItems.Any())
            {
                return NotFound("No items in cart");
            }

            _context.CartItems.RemoveRange(cartItems);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CartItemExists(int id)
        {
            return _context.CartItems.Any(e => e.Id == id);
        }
    }
}