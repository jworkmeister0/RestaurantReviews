namespace RestaurantReviews.Models
{
    public class Review
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string? ReviewText { get; set; }
        public int Rating { 
            get
            {
                return Rating;
            }
            set
            {
                if (value > 10)
                {
                    Rating = 10;
                } else if (value < 0)
                {
                    Rating = 0;
                }
            }
        }
    }
}
