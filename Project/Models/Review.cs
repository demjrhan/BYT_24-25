using Project.Entities;

namespace Project.Models
{
    public class Review
    {

        private static int _lastId = 0;
        private static List<Review> Instances = new List<Review>();
        private Product _product;
        private Customer _customer;
        public int ReviewId { get; private set; } = _lastId++;

        public int Rating { get; set; }
        private int IssuedBy { get; set; }
        public string? Comment { get; set; }
        public int ProductId {  get; set; }


        public Review(Customer customer, Product product, int rating, string? comment = "")
        {
            if (rating < 1 || rating > 5)
            {
                throw new ArgumentException("Rating must be between 1 and 5.");
            }

            IssuedBy = customer.CustomerId;
            _customer = customer;
            Rating = rating;
            Comment = comment;
            _product = product;
            ProductId = product.ProductId;
            
            customer.AddReview(this);

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
            review._customer.RemoveReview(review);
            review._product.RemoveReview(review);
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

