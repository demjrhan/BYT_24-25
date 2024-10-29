using Project;

namespace Project_Tests
{
    public class ProductTests
    {
        [Test]
        public void ApplyPromotion()
        {
            var product = new Book("Book", 100, 5, "Author", "Fantasy", 2020);
            var promotion = new Promotion("Test Promotion", "20% off", 20, product.ProductId);
            double discountedPrice = product.ApplyPromotion(promotion);
            Assert.That(discountedPrice, Is.EqualTo(80));
        }

        [Test]
        public void AddPromotion()
        {
            var product = new Book("Book", 20, 5, "Author", "Fantasy", 2020);

            product.AddPromotion("Test Promotion", "10% off", 10);
            Console.WriteLine(product.Promotions.Count);
            var promotion = product.Promotions[0];

            Assert.That(product.Promotions.Count, Is.EqualTo(1));
            Assert.That(product.Promotions[0], Is.EqualTo(promotion));
        }

        [Test]
        public void RemovePromotion()
        {
            var product = new Book("Book", 20, 5, "Author", "Fantasy", 2020);
            product.AddPromotion("Test Promotion", "10% off", 10);
            var promotion = product.Promotions[0];

            product.RemovePromotion(promotion);

            Assert.That(product.Promotions.Count, Is.EqualTo(0));
        }


    }

    
}