namespace Project.Models
{
    public class Review
    {

        private static int _lastId = 0;
        private static List<Review> Instances = new List<Review>();
        public int ReviewId { get; private set; } = _lastId++;

        public int Rating { get; set; }
        public string? Comment { get; set; }
        public int IssuedBy { get; set; }
        public int ProductId {  get; set; }
        private Product Product { get; set; }


        public Review(int customerId, Product product, int rating, string? comment = "")
        {
            if (rating < 1 || rating > 5)
            {
                throw new ArgumentException("Rating must be between 1 and 5.");
            }

            IssuedBy = customerId;
            Rating = rating;
            Comment = comment;
            Product = product;
            ProductId = product.ProductId;
            Product.AddReviewToProduct(product, this);
            Instances.Add(this);
        }
        public static IReadOnlyList<Review> GetInstances()
        {
            return Instances.AsReadOnly();
        }
        
        public static void ClearInstances()
        {
            Instances.Clear();
        }

        public static bool Exists(Review givenReview)
        {
            foreach (var review in Instances)
            {
                if (review == givenReview)
                    return true;
            }
            return false;
        }
        public static void RemoveReview(Review review)
        {
            review.Product.RemoveReview(review);
            Instances.Remove(review);
        }

        public static void PrintInstances()
        {
            foreach (var review in Instances)
            {
                Console.WriteLine(review.ToString());
            }
        }

    }
}

