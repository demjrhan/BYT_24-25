using Project;

var person = new Young("Alice", "Johnson", "alice.johnson@example.com", 
    "123-456-7890", "123 Main St", 15, false);

person.DisplayRoles(); 
person.AddCustomerRole();


var payment = person.CreatePayment("Credit Card", 100.50);
Console.WriteLine($"Payment created: {payment.PaymentMethod} - ${payment.Amount}"); 

var review = person.CreateReview(5, "Great service!");
Console.WriteLine($"Review created: {review.Rating} stars - {review.Comment}"); 

person.AddEmployeeRole("Sales Manager", DateTime.Now, 60000);

try
{
    var report = person.CreateReport("Monthly Sales", "Sales report content here.");
    Console.WriteLine($"Report created: {report.ReportType} - {report.Content}"); 
}
catch (InvalidOperationException ex)
{
    Console.WriteLine(ex.Message);
}

var allPersons = Person.GetAllPersons();
Console.WriteLine("All Persons:");
foreach (var p in allPersons)
{
    p.DisplayRoles();
    Console.WriteLine(p.GetDiscountPercentage());
}

/* Serialization example
List<Young> youngPersons = new List<Young>();
youngPersons.Add(person);
SerializeDeserialize.SerializeToFile(youngPersons, "young.json"); */

/* Deserialization example
List<Young> youngPersons = SerializeDeserialize.DeserializeFromFile<Young>("young.json"); */