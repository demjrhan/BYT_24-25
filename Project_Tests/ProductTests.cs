using Project;

namespace Project_Tests
{
    public class ProductTests
    {
        [Test]
        //Should return correct discounted price
        public void ApplyPromotion()
        {
            var product = new Book("Book", 100, 5, "Author", "Fantasy", 2020);
            var promotion = new Promotion("Test Promotion", "20% off", 20);
            double discountedPrice = product.ApplyPromotion(promotion);
            Assert.AreEqual(80, discountedPrice);
        }

        [Test]
        //Should add promotion successfully
        public void AddPromotion()
        {
            var product = new Book("Book", 20, 5, "Author", "Fantasy", 2020);
            var promotion = new Promotion("Test Promotion", "10% off", 10);

            product.AddPromotion(promotion);

            Assert.AreEqual(1, product.Promotions.Count);
            Assert.AreEqual(promotion, product.Promotions[0]);
        }

        [Test]
        //Should remove promotion successfully
        public void RemovePromotion()
        {
            var product = new Book("Book", 20, 5, "Author", "Fantasy", 2020);
            var promotion = new Promotion("Test Promotion", "10% off", 10);
            product.AddPromotion(promotion);

            product.RemovePromotion(promotion);

            Assert.AreEqual(0, product.Promotions.Count);
        }


    }

    
}