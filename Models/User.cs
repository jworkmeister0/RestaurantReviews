namespace RestaurantReviews.Models
{
    public class User
    {
        public long Id { get; set; }
        public string? UserName { get; set; }
        public string? Name { get; set; }
        public bool IsBlocked { get; set; }
    }
}
