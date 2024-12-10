
using Project.Entities;
using Project.Features;
using Project.Models;
using System.Reflection;

namespace Project_Tests
{
    
    public class TestProduct : Product
    {
        public TestProduct(string title, double price, int quantity) : base(title, price, quantity) { }
    }
    [TestFixture]
    public class ProductTests
    {
        [Test]
        public void ProductCreation_ShouldInitializeCorrectly()
        {
            var product = new TestProduct("The Great Gatsby", 20.0, 50);

            Assert.That(product.Title, Is.EqualTo("The Great Gatsby"));
            Assert.That(product.Price, Is.EqualTo(20.0));
            Assert.That(product.StockQuantity, Is.EqualTo(50));
            Assert.That(product.ProductId, Is.GreaterThan(0));
        }

        [Test]
        public void ProductCreation_InvalidPrice_ShouldThrowException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                new TestProduct("The Great Gatsby", -20.0, 50);
            });
        }

        [Test]
        public void ProductCreation_InvalidStockQuantity_ShouldThrowException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                new TestProduct("The Great Gatsby", 20.0, -5);
            });
        }

        [Test]
        public void AddReviewToProduct_ShouldIncreaseReviewsCount()
        {
            var customer = new Customer("John", "Doe", "john.doe@example.com", "123456789", "123 Elm St", 30, false, true, false);
            var product = new TestProduct("The Great Gatsby", 20.0, 50);
            var review = new Review(customer, product, 5, "Amazing book!");

            var reviewsField = typeof(Product).GetField("_reviews", BindingFlags.Instance | BindingFlags.NonPublic);
            var reviews = reviewsField!.GetValue(product) as List<Review>;

            Assert.That(product.CountReviews(), Is.EqualTo(1));
            Assert.That(reviews![0], Is.EqualTo(review));
        }

        [Test]
        public void AddPromotionToProduct_ShouldIncreasePromotionsCount()
        {
            var product = new TestProduct("The Great Gatsby", 20.0, 50);
            var promotion = new Promotion("Summer Sale", "20% off", 20.0, product);

            var promotionsField = typeof(Product).GetField("_promotions", BindingFlags.Instance | BindingFlags.NonPublic);
            var promotions = promotionsField!.GetValue(product) as List<Promotion>;

            Assert.That(product.CountPromotions(), Is.EqualTo(1));
            Assert.That(promotions![0], Is.EqualTo(promotion));
        }
        
        [Test]
        public void RemovePromotionFromProduct_ShouldRemovePromotion()
        {
            var product = new TestProduct("The Great Gatsby", 20.0, 50);
            var promotion = new Promotion("Summer Sale", "20% off", 20.0, product);
            product.RemovePromotion(promotion);

            Assert.That(product.CountPromotions(), Is.EqualTo(0));
        }

        [Test]
        public void ApplyPromotion_ShouldApplyDiscount()
        {
            var product = new TestProduct("The Great Gatsby", 20.0, 50);
            var promotion = new Promotion("Summer Sale", "20% off", 20.0, product);

            var discountedPrice = product.ApplyPromotion(promotion);

            Assert.That(discountedPrice, Is.EqualTo(16.0));
        }

        [Test]
        public void ProductExists_ShouldReturnTrueForExistingProduct()
        {
            var product = new TestProduct("The Great Gatsby", 20.0, 50);

            Assert.IsTrue(TestProduct.Exists(product.ProductId));
        }

        [Test]
        public void ProductExists_ShouldReturnFalseForNonExistingProduct()
        {
            Assert.IsFalse(TestProduct.Exists(9999));
        }
    }

    
}