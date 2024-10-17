namespace Project;

public class PersonWithRoles
{
    public Person Person { get; private set; }
    public CustomerRole? CustomerRole { get; private set; }
    public EmployeeRole? EmployeeRole { get; private set; }

    public PersonWithRoles(Person person)
    {
        Person = person;
    }

    public void AddCustomerRole()
    {
        if (CustomerRole == null)
            CustomerRole = new CustomerRole();
    }

    public void AddEmployeeRole(string position, DateTime hireDate, double? salary = null)
    {
        if (EmployeeRole == null)
            EmployeeRole = new EmployeeRole(position, hireDate, salary);
    }
}