namespace Project;

public abstract class Person
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public int Age { get; set; }

    protected static List<Person> personExtent = new List<Person>();

    protected Person(string name, string surname, string email, string phone, string address, int age)
    {
        Name = name;
        Surname = surname;
        Email = email;
        Phone = phone;
        Address = address;
        Age = age;

        
    }

    public static IReadOnlyList<Person> GetAllPersons() => personExtent.AsReadOnly();
    public abstract double GetDiscountPercentage();
}