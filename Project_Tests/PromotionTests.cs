
using Project;
using Project.Features;

namespace Project_Tests
{
    public class PromotionTests
    {
        [Test]
        public void PromotionCreation()
        {
            var promotion = new Promotion("Summer Sale", "20% off", 20, 0);

            Assert.That(promotion.Name, Is.EqualTo("Summer Sale"));
            Assert.That(promotion.Description, Is.EqualTo("20% off"));
            Assert.That(promotion.DiscountPercentage, Is.EqualTo(20));
        }

        [Test]
        public void Test_ToString()
        {
            var promotion = new Promotion("Summer Sale", "20% off", 20, 0);

            string result = promotion.ToString();

            Assert.IsTrue(result.Contains("Discount: 20"));
        }
    }
}
