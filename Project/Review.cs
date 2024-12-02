namespace Project
{
    public class Review
    {
        private static int _lastId = 0;
        private static List<Review> Instances = [];

        public int ReviewId { get; private set; } = _lastId++;
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public int IssuedBy { get; set; }
        private Product Product { get; set; }

        public Review(int customerId, int rating, Product product, string? comment = "")
        {
            if (Rating < 1 || Rating > 5)
            {
                throw new ArgumentException("Rating must be between 1 and 5.");
            }

            IssuedBy = customerId;
            Rating = rating;
            Comment = comment;
            Product = product;

            Product.AddReviewToProduct(product, this);
            Instances.Add(this);
        }

        public static void RemoveReview(Review review)
        {
            Instances.Remove(review);
        }

        public static void PrintInstances()
        {
            foreach (var i in Instances)
            {
                Console.WriteLine(i.ToString());
            }
        }
    }

}

