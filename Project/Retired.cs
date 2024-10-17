namespace Project;

public enum RetirementType
{
    Military,
    HealthIssues,
    Other
}

public class Retired : Person
{
    public RetirementType RetirementType { get; set; }

    public Retired(string name, string surname, string email, string phone, string address, int age, RetirementType typeOfRetirement)
        : base(name, surname, email, phone, address, age)
    {
        RetirementType = typeOfRetirement;
    }

    
    public override double GetDiscountPercentage()
    {
        if (RetirementType == RetirementType.Military)
        {
            return 15.0;
        }
        else if (RetirementType == RetirementType.HealthIssues)
        {
            return 15.0;
        }
        else return 5.0;

    }
}
