namespace Project;

public class Adult : Person
{
    public bool IsWorking { get; set; }

    public Adult(string name, string surname, string email, string phone, string address, int age, bool isWorking)
        : base(name, surname, email, phone, address, age)
    {
        IsWorking = isWorking;
    }
    public override double GetDiscountPercentage()
    {
        return IsWorking ? 5.0 : 0.0; 
    }
}
