using Project.Controller;

Controller.Start();



//Inventory.InventoryInfo();
//Console.WriteLine("\n========== Adding Products ================");
//Book book1 = new Book("Harry Potter", 25, 300, "J.K.Rowling", "Fantasy", 2000);
//Console.WriteLine(book1);
//Book book2 = new Book("Harry Potter", 25, 300, "J.K.Rowling", "Fantasy", 2001);
//Book book3 = new Book("Harry Potter", 25, 300, "J.K.Rowling", "Fantasy", 2002);
//Book book4 = new Book("Harry Potter", 25, 300, "J.K.Rowling", "Fantasy", 2001);
//Book book5 = new Book("Harry Potter", 25, 300, "J.K.Rowling", "Fantasy", 2003);
//Book book6 = new Book("Harry Potter", 25, 300, "J.K.Rowling", "Fantasy", 2001);
//List<Product> products = new List<Product>();
//Accessory ac1 = new Accessory("lance", 120, 10, "brand", "silver");
//Accessory ac2 = new Accessory("charm", 120, 10, "brand", "wood");
//Accessory ac3 = new Accessory("pen", 120, 10, "brand", "metal");
//Accessory ac4 = new Accessory("book-cover", 120, 10, "brand", "leather");

//products.Add(book1);
//products.Add(book2);
//products.Add(book3);
//products.Add(book4);
//products.Add(book5);
//products.Add(book6);
//products.Add(ac1);
//products.Add(ac2);
//products.Add(ac3);
//products.Add(ac4);

//foreach (Product product in products)
//{
//    Console.WriteLine(product.ToString());
//}

//Inventory.InventoryInfo();
//Console.WriteLine("\n============= Adding Promotions ================");
//book1.AddPromotion(new Promotion("Seasonal Sale", "25% discount", 25));
//Promotion p2 = new Promotion("Bundle", "50% discount for five books", 50);
//book1.AddPromotion(p2);
//book2.AddPromotion(p2);
//book3.AddPromotion(p2);
//book4.AddPromotion(p2);
//book5.AddPromotion(p2);


//Console.WriteLine("\n============ Adding customers ===============");
//Customer c1 = new Customer(
//    "Danila", "Zakharchuk", 
//    "mail", "phone", 
//    "address", 24,
//    true, false,
//    false);
//Customer c2 = new Customer(
//    "Pavel", "Maevskiy",
//    "mail1", "phone1",
//    "address1", 23,
//    false, true,
//    false);
//Customer c3 = new Customer(
//    "Ivan", "Karpovich",
//    "mail2", "phone2",
//    "address2", 24,
//    true, true,
//    false);
//Customer c4 = new Customer(
//    "Ilya", "Stepanov",
//    "mail3", "phone3",
//    "address3", 24,
//    false, false,
//    false);
//Customer c5 = new Customer(
//    "Natalia", "Ivanovna",
//    "mail4", "phone4",
//    "address4", 80,
//    false, false,
//    true, Retirement.HealthIssues);

//foreach (var c in Customer.GetAllCustomers())
//{
//    Console.WriteLine(c.ToString());
//}


//Console.WriteLine("\n============= Adding employees ==============");
//Employee e1 = new Employee(
//    "Alexey", "Ermolenkov",
//    "mail", "phone",
//    "address", 25,
//    false, true,
//    false, Position.Manager,
//    new DateTime(2015, 12, 25), 3000);
//Employee e2 = new Employee(
//    "Danil", "Petrachkov",
//    "mail1", "phone1",
//    "address1", 24,
//    false, true,
//    false, Position.Assistant,
//    new DateTime(2015, 12, 25), 2500);

//e1.Subordinate = e2;
//foreach (var e in Employee.GetAllEmployees())
//{
//    Console.WriteLine(e.ToString());
//}

//Console.WriteLine("\n========= Adding report ==============");
//Report r1 = e1.CreateReport("Stock Update", "Added new collection of books (300)");
//Console.WriteLine(r1.ToString());

//c1.Membership = new Membership(c1.CustomerId, true, 15);

//Console.WriteLine("\n============ Filling cart =============");
//c1.Cart.AddProduct(ac1);
//c1.Cart.AddProduct(book1, book1.Promotions[0]);
//Console.WriteLine(c1.Cart.Products.Count);
//Console.WriteLine(c1.Cart.ToString());

//Console.WriteLine("\n============== Creating Order ============");
//Order o1 = c1.Cart.ConvertToOrder();
//Console.WriteLine(o1.ToString());
//Inventory.InventoryInfo();

//Console.WriteLine("\n============== Creating Shipping ============");
//Shipping s1 = o1.CreateShipping("Paczkomat", 10, "Koszykowa 76");
//Console.WriteLine(s1.ToString());

//Console.WriteLine("\n============== Creating Payment ============");
//Payment payment1 = c1.CreatePayment(o1.OrderId, "card", o1.Amount);
//Console.WriteLine(payment1.ToString());

//Console.WriteLine("\n============== Checking Serialization ============");

//Console.WriteLine(Directory.GetCurrentDirectory());

//Serialization example
//SerializeDeserialize.SerializeToFile(Customer.Customers, "customers.json");

//Deserialization example
//List<Customer> customers = SerializeDeserialize.DeserializeFromFile<Customer>("customers.json");
//foreach(var customer in customers)
//{
//    Console.WriteLine(customer.ToString());
//}