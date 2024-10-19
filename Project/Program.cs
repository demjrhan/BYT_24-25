using Project;

Book book1 = new Book("Harry Potter", 25, 300, "J.K.Rowling", "Fantasy", 2000);
Book book2 = new Book("Harry Potter", 25, 300, "J.K.Rowling", "Fantasy", 2001);
Book book3 = new Book("Harry Potter", 25, 300, "J.K.Rowling", "Fantasy", 2002);
Book book4 = new Book("Harry Potter", 25, 300, "J.K.Rowling", "Fantasy", 2001);
Book book5 = new Book("Harry Potter", 25, 300, "J.K.Rowling", "Fantasy", 2003);
Book book6 = new Book("Harry Potter", 25, 300, "J.K.Rowling", "Fantasy", 2001);
List<Product> products = new List<Product>();
Accessory ac1 = new Accessory("lance", 120, 10, "brand", "metal");

products.Add(book1);
products.Add(book2);
products.Add(book3);
products.Add(ac1);

foreach (Product product in products)
{
    Console.WriteLine(product.ToString());
}

Console.WriteLine(book4.ToString());