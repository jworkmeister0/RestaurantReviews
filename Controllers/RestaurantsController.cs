using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantReviews.Models;

namespace RestaurantReviews.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        private readonly RestaurantReviewContext _context;

        public RestaurantsController(RestaurantReviewContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Restaurant>>> GetRestaurants()
        {
          if (_context.Restaurants == null)
          {
              return NotFound();
          }
            return await _context.Restaurants.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Restaurant>> GetRestaurant(long id)
        {
          if (_context.Restaurants == null)
          {
              return NotFound();
          }
            var restaurant = await _context.Restaurants.FindAsync(id);

            if (restaurant == null)
            {
                return NotFound();
            }

            return restaurant;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRestaurant(long id, Restaurant restaurant)
        {
            if (id != restaurant.Id)
            {
                return BadRequest();
            }

            _context.Entry(restaurant).State = EntityState.Modified;

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

        [HttpPost]
        public async Task<ActionResult<Restaurant>> PostRestaurant(Restaurant restaurant)
        {
          if (_context.Restaurants == null)
          {
              return Problem("Entity set 'RestaurantContext.Restaurants'  is null.");
          }
            _context.Restaurants.Add(restaurant);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRestaurant), new { id = restaurant.Id }, restaurant);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurant(long id)
        {
            if (_context.Restaurants == null)
            {
                return NotFound();
            }
            var restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant == null)
            {
                return NotFound();
            }

            _context.Restaurants.Remove(restaurant);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RestaurantExists(long id)
        {
            return (_context.Restaurants?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
