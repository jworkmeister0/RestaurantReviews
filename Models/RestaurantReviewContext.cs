using Microsoft.EntityFrameworkCore;

namespace RestaurantReviews.Models
{
    public class RestaurantReviewContext : DbContext
    {
        public RestaurantReviewContext(DbContextOptions<RestaurantReviewContext> options) : base(options)
        {
        }

        public DbSet<City> Cities { get; set; } = null!;
        public DbSet<Restaurant> Restaurants { get; set; } = null!;
        public DbSet<Review> Reviews { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
    }
}
