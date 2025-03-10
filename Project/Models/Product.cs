﻿using Project.Entities;
using Project.Features;

namespace Project.Models
{
    public abstract class Product
    {
        private static int _lastId = 0;
        private static List<Product> Instances = new List<Product>();
        private List<Promotion> _promotions = new List<Promotion>();
        private List<Review> _reviews = new List<Review>();
        private List<Cart> _carts = new List<Cart>();
        private List<Customer> _inFavorites = new List<Customer>();

        private string _title = null!;
        private double _price;
        private int _stockQuantity;
        public int ProductId { get; private set; }  

        public string Title 
        { 
            get => _title; 
            set
            {
                ValidateTitle(value);
                _title = value;
            }
        }

        public double Price 
        { 
            get => _price; 
            set
            {
                ValidatePrice(value);
                _price = value;
            }
        }

        public int StockQuantity
        {
            get => _stockQuantity;
            set
            {
                ValidateStockQuantity(value);
                _stockQuantity = value;
            }
        }
        public Product(string title, double price, int quantity)
        {
            ValidateTitle(title);
            ValidatePrice(price);
            ValidateStockQuantity(quantity);

            Title = title;
            Price = price;
            StockQuantity = quantity;

            ProductId = _lastId++;
            Instances.Add(this);

        }
        public static IReadOnlyList<Product> GetInstances()
        {
            return Instances.AsReadOnly();
        }
        
        public static void ClearInstances()
        {
            Instances.Clear();
        }

        public static bool Exists(Product givenProduct)
        {
            foreach (var product in Instances)
            {
                if (product == givenProduct)
                    return true;
            }
            return false;
        }
        public static void AddReviewToProduct(Product product, Review review)
        {
            product?._reviews.Add(review);
        }

        public static void AddPromotionToProduct(Product product, Promotion promotion)
        {
            product?._promotions.Add(promotion);
        }

        public void RemovePromotion(Promotion promotion)
        {
            if (promotion == null)
                throw new ArgumentNullException(nameof(promotion), "Promotion cannot be null.");
            _promotions.Remove(promotion);
        }

        public void RemoveReview(Review review)
        {
            _reviews.Remove(review);
        }

        public double ApplyPromotion(Promotion promotion)
        {
            if (promotion == null)
                throw new ArgumentNullException(nameof(promotion), "Promotion cannot be null.");
            return Price - (Price * (promotion.DiscountPercentage / 100));
        }

        public static void PrintInstances()
        {
            foreach (var product in Instances)
            {
                Console.WriteLine(product.ToString());
            }
        }
        
        // Validation methods added seperately to maintain reusability and readability.
        private static void ValidateTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be null or empty.", nameof(title));
        }

        private static void ValidatePrice(double price)
        {
            if (price < 0)
                throw new ArgumentOutOfRangeException(nameof(price), "Price cannot be negative.");
        }

        private static void ValidateStockQuantity(int quantity)
        {
            if (quantity < 0)
                throw new ArgumentOutOfRangeException(nameof(quantity), "Stock quantity cannot be negative.");
        }
        public static bool Exists(int id)
        {
            foreach (var product in Instances)
            {
                if (product.ProductId == id)
                    return true;
            }
            return false;
        }

        public void AddCart(Cart cart)
        {
            _carts.Add(cart);
        }

        public void RemoveCart(Cart cart)
        {
            _carts.Remove(cart);
        }

        public static void RemoveProduct(Product product)
        {
            foreach(var review in product._reviews)
            {
                Review.RemoveReview(review);
            }
            product._reviews.Clear();
            foreach (var promotion in product._promotions)
            {
                Promotion.RemovePromotion(promotion);
            }
            product._promotions.Clear();
            foreach (var cart in product._carts)
            {
                cart.RemoveProduct(product);
            }
            product._carts.Clear();
            foreach (var customer in product._inFavorites)
            {
                customer.RemoveFromFavorites(product);
            }
            product._inFavorites.Clear();

            Instances.Remove(product);
        }

        public bool ContainPromotion(Promotion promotion)
        {
            return this._promotions.Contains(promotion);
        }

        public int CountReviews()
        {
            return _reviews.Count;
        }

        public int CountPromotions()
        {
            return _promotions.Count;
        }

        public void AddCustomerToFavorites(Customer customer)
        {
            _inFavorites.Add(customer);
        }

        public void RemoveCustomerFromFavorites(Customer customer)
        {
            _inFavorites.Remove(customer);
        }

        public override string ToString()
        {
            return $"Id: {ProductId}, Type: {GetType().Name}, Title: {Title}";
        }
    }

}

