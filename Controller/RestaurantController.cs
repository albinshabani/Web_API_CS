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
    public class RestaurantController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RestaurantController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Restaurant
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetRestaurants()
        {
            return await _context.Restaurants
                .Select(r => new RestaurantDto
                {
                    Id = r.Id,
                    Location = r.Location,
                    PhoneNumber = r.PhoneNumber,
                    EmailAddress = r.EmailAddress,
                    OpeningHours = r.OpeningHours,
                    CreatedAt = r.CreatedAt
                })
                .ToListAsync();
        }

        // GET: api/Restaurant/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RestaurantDto>> GetRestaurant(int id)
        {
            var restaurant = await _context.Restaurants.FindAsync(id);

            if (restaurant == null)
            {
                return NotFound();
            }

            return new RestaurantDto
            {
                Id = restaurant.Id,
                Location = restaurant.Location,
                PhoneNumber = restaurant.PhoneNumber,
                EmailAddress = restaurant.EmailAddress,
                OpeningHours = restaurant.OpeningHours,
                CreatedAt = restaurant.CreatedAt
            };
        }

        // POST: api/Restaurant
        [HttpPost]
        public async Task<ActionResult<RestaurantDto>> CreateRestaurant(CreateRestaurantDto createRestaurantDto)
        {
            var restaurant = new Restaurant
            {
                Location = createRestaurantDto.Location,
                PhoneNumber = createRestaurantDto.PhoneNumber,
                EmailAddress = createRestaurantDto.EmailAddress,
                OpeningHours = createRestaurantDto.OpeningHours,
                CreatedAt = DateTime.UtcNow
            };

            _context.Restaurants.Add(restaurant);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRestaurant), new { id = restaurant.Id }, new RestaurantDto
            {
                Id = restaurant.Id,
                Location = restaurant.Location,
                PhoneNumber = restaurant.PhoneNumber,
                EmailAddress = restaurant.EmailAddress,
                OpeningHours = restaurant.OpeningHours,
                CreatedAt = restaurant.CreatedAt
            });
        }

        // PUT: api/Restaurant/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRestaurant(int id, UpdateRestaurantDto updateRestaurantDto)
        {
            var restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant == null)
            {
                return NotFound();
            }

            restaurant.Location = updateRestaurantDto.Location ?? restaurant.Location;
            restaurant.PhoneNumber = updateRestaurantDto.PhoneNumber ?? restaurant.PhoneNumber;
            restaurant.EmailAddress = updateRestaurantDto.EmailAddress ?? restaurant.EmailAddress;
            restaurant.OpeningHours = updateRestaurantDto.OpeningHours ?? restaurant.OpeningHours;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RestaurantExists(id))
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

        // DELETE: api/Restaurant/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurant(int id)
        {
            var restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant == null)
            {
                return NotFound();
            }

            _context.Restaurants.Remove(restaurant);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RestaurantExists(int id)
        {
            return _context.Restaurants.Any(e => e.Id == id);
        }
    }
}