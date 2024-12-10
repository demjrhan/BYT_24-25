
using Project.Models;

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
            var product = new TestProduct("The Great Gatsby", 20.0, 50);
            var review = product.AddReview(1, 5, "Amazing book!");

            Assert.That(product.Reviews.Count, Is.EqualTo(1));
            Assert.That(product.Reviews[0], Is.EqualTo(review));
        }

        [Test]
        public void AddPromotionToProduct_ShouldIncreasePromotionsCount()
        {
            var product = new TestProduct("The Great Gatsby", 20.0, 50);
            var promotion = product.AddPromotion("Summer Sale", "20% off", 20.0);

            Assert.That(product.Promotions.Count, Is.EqualTo(1));
            Assert.That(product.Promotions[0], Is.EqualTo(promotion));
        }
        
        [Test]
        public void RemovePromotionFromProduct_ShouldRemovePromotion()
        {
            var product = new TestProduct("The Great Gatsby", 20.0, 50);
            var promotion = product.AddPromotion("Summer Sale", "20% off", 20.0);
            product.RemovePromotion(promotion);

            Assert.That(product.Promotions.Count, Is.EqualTo(0));
        }

        [Test]
        public void ApplyPromotion_ShouldApplyDiscount()
        {
            var product = new TestProduct("The Great Gatsby", 20.0, 50);
            var promotion = product.AddPromotion("Summer Sale", "20% off", 20.0);

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