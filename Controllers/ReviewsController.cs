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
    public class ReviewsController : ControllerBase
    {
        private readonly RestaurantReviewContext _context;

        public ReviewsController(RestaurantReviewContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Review>>> GetReviews()
        {
          if (_context.Reviews == null)
          {
              return NotFound();
          }
            return await _context.Reviews.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Review>> GetReview(long id)
        {
          if (_context.Reviews == null)
          {
              return NotFound();
          }
            var review = await _context.Reviews.FindAsync(id);

            if (review == null)
            {
                return NotFound();
            }

            return review;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutReview(long id, Review review)
        {
            if (id != review.Id)
            {
                return BadRequest();
            }

            _context.Entry(review).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReviewExists(id))
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
        public async Task<ActionResult<Review>> PostReview(Review review)
        {
          if (_context.Reviews == null)
          {
              return Problem("Entity set 'RestaurantReviewContext.Reviews'  is null.");
          }
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReview", new { id = review.Id }, review);
        }

        [HttpDelete("{id}")]
        // TODO: Don't actually delete, mark disabled in db
        public async Task<IActionResult> DeleteReview(long id)
        {
            if (_context.Reviews == null)
            {
                return NotFound();
            }
            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReviewExists(long id)
        {
            return (_context.Reviews?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
