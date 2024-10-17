namespace Project;

public class Young : Person
{
    public bool IsStudying { get; set; }

    public Young(string name, string surname, string email, string phone, string address, int age, bool isStudying)
        : base(name, surname, email, phone, address, age)
    {
        IsStudying = isStudying;
    }

    public override double GetDiscountPercentage()
    {
        return IsStudying ? 15.0 : 5; 
    }
}
