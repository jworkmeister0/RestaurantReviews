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
    public class CitiesController : ControllerBase
    {
        private readonly RestaurantReviewContext _context;

        public CitiesController(RestaurantReviewContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<City>>> GetCities()
        {
          if (_context.Cities == null)
          {
              return NotFound();
          }
            return await _context.Cities.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<City>> GetCity(long id)
        {
          if (_context.Cities == null)
          {
              return NotFound();
          }
            var city = await _context.Cities.FindAsync(id);

            if (city == null)
            {
                return NotFound();
            }

            return city;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCity(long id, City city)
        {
            if (id != city.Id)
            {
                return BadRequest();
            }

            _context.Entry(city).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CityExists(id))
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
        public async Task<ActionResult<City>> PostCity(City city)
        {
          if (_context.Cities == null)
          {
              return Problem("Entity set 'RestaurantReviewContext.Cities'  is null.");
          }
            _context.Cities.Add(city);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCity", new { id = city.Id }, city);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(long id)
        {
            if (_context.Cities == null)
            {
                return NotFound();
            }
            var city = await _context.Cities.FindAsync(id);
            if (city == null)
            {
                return NotFound();
            }

            _context.Cities.Remove(city);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CityExists(long id)
        {
            return (_context.Cities?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
