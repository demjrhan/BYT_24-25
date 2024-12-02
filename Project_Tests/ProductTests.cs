using Project;
using Project.Features;
using Project.Models;
using Project.Utilities;

namespace Project_Tests
{
    public class ProductTests
    {
        [SetUp]
        public void Setup()
        {
            SerializeDeserialize.ResetInstances<Product>(typeof(Product));
            SerializeDeserialize.ResetInstances<Book>(typeof(Book));
            SerializeDeserialize.ResetInstances<Promotion>(typeof(Promotion));
        }

        [Test]
        public void Test_ApplyPromotion()
        {
            var product = new Book("Book", 100, 5, "Author", "Fantasy", 2020);
            var promotion = new Promotion("Test Promotion", "20% off", 20, product.ProductId);
            double discountedPrice = product.ApplyPromotion(promotion);
            Assert.That(discountedPrice, Is.EqualTo(80));
        }

        [Test]
        public void Test_AddPromotion()
        {
            var product = new Book("Book", 20, 5, "Author", "Fantasy", 2020);

            product.AddPromotion("Test Promotion", "10% off", 10);
            var promotion = product.Promotions[0];

            Assert.That(product.Promotions.Count, Is.EqualTo(1), "Count should be equal 1");
            Assert.That(product.Promotions[0], Is.EqualTo(promotion), "Extent should contaain same instance");
        }

        [Test]
        public void Test_RemovePromotion()
        {
            var product = new Book("Book", 20, 5, "Author", "Fantasy", 2020);
            product.AddPromotion("Test Promotion", "10% off", 10);
            var promotion = product.Promotions[0];
            product.RemovePromotion(promotion);

            Assert.That(product.Promotions.Count, Is.EqualTo(0), "After Removal Count should be 0");
        }


    }

    
}