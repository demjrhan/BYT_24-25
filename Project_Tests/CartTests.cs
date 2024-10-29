
using Project;

namespace Project_Tests
{
    public class CartTests
    {

       [Test]
       public void CalculateTotalSum()
       {

           var customer = new  Customer(
               "Ilya", "Stepanov",
               "mail3", "phone3",
               "address3", 24,
               false, false,
               false);
           
           var cart = new Cart(0);
           var product = new Book("Book", 100, 10, "Author", "Genre", 2022);
           var promotion = new Promotion("Discount", "10% off", 10, product.ProductId);
           cart.AddProduct(product, promotion);

           double totalSum = cart.CalculateTotalSum();

           Assert.That(totalSum, Is.EqualTo(90)); // 100 - 10% = 90
       }

    }
}
