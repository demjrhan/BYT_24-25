using NUnit.Framework;
using Project.Models;
using Project.Features;
using System;

namespace Project_Tests
{
    [TestFixture]
    public class PromotionTests
    {
        [Test]
        public void PromotionCreation_ShouldInitializeCorrectly()
        {
            var product = new TestProduct("The Great Gatsby", 20.0, 50);
            var promotion = new Promotion("Holiday Sale", "10% off", 10.0, product);

            Assert.That(promotion.Name, Is.EqualTo("Holiday Sale"));
            Assert.That(promotion.Description, Is.EqualTo("10% off"));
            Assert.That(promotion.DiscountPercentage, Is.EqualTo(10.0));
            Assert.That(promotion.ProductId, Is.EqualTo(product.ProductId));
        }

        [Test]
        public void ApplyPromotion_ShouldApplyDiscountCorrectly()
        {
            var product = new TestProduct("The Great Gatsby", 20.0, 50);
            var promotion = new Promotion("Holiday Sale", "10% off", 10.0, product);

            var discountedPrice = product.ApplyPromotion(promotion);

            Assert.That(discountedPrice, Is.EqualTo(18.0)); // 20 - 10% = 18
        }

        [Test]
        public void RemovePromotion_ShouldRemovePromotionFromProduct()
        {
            var product = new TestProduct("The Great Gatsby", 20.0, 50);
            var promotion = new Promotion("Holiday Sale", "10% off", 10.0, product);

            product.RemovePromotion(promotion);

            Assert.That(product.Promotions.Count, Is.EqualTo(0));
        }

        [Test]
        public void AddPromotionToProduct_ShouldIncreasePromotionCount()
        {
            var product = new TestProduct("The Great Gatsby", 20.0, 50);
            var promotion = new Promotion("Holiday Sale", "10% off", 10.0, product);

            Assert.That(product.Promotions.Count, Is.EqualTo(1));
            Assert.That(product.Promotions[0], Is.EqualTo(promotion));
        }

        [Test]
        public void PromotionCreation_InvalidDiscountPercentage_ShouldThrowException()
        {
            var product = new TestProduct("The Great Gatsby", 20.0, 50);

            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                new Promotion("Holiday Sale", "Negative Discount", -10.0, product);
            });
        }
    }
}
