namespace Project;

public class EmployeeRole 
{
    
    private static int _lastEmployeeId = 0;
    public int EmployeeId { get; private set; }
    public string Position { get; private set; }
    public DateTime HireDate { get; private set; }
    public double? Salary { get; private set; }
    public List<EmployeeRole>? Managers { get; set; }

    public EmployeeRole(string position, DateTime hireDate, double? salary = null)
    {
        EmployeeId = ++_lastEmployeeId;
        Position = position;
        HireDate = hireDate;
        Salary = salary;
    }

    public Report CreateReport(string reportType, string content)
    {
        if (Position.ToLower().Contains("manager"))
        {
            return new Report(reportType, content, DateTime.Now);
        }
        else
        {
            throw new InvalidOperationException("Only managers can create reports.");
        }
    }
}