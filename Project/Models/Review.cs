namespace Project.Models
{
    public class Review
    {
        public int Rating { get; set; }
        public string Comment { get; set; }
        public int IssuedBy { get; set; }

        public Review(int customerId, int rating, string comment)
        {
            if (Rating < 1 || Rating > 5)
            {
                throw new ArgumentException("Rating must be between 1 and 5.");
            }

            IssuedBy = customerId;
            Rating = rating;
            Comment = comment;
        }
    }

}

