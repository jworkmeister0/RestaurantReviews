namespace RestaurantReviews.Models
{
    public class Restaurant
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public long CityId { get; set; }
    }
}
