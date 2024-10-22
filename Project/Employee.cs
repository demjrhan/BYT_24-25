namespace Project
{
    public enum Position
    {
        Manager,
        Assistant
    }

    public class Employee : Person
    {
        private static int _lastEmployeeId = 0;
        public static List<Employee> Employees = new List<Employee>();

        public int EmployeeId { get; private set; }
        public Position EmpPosition { get; private set; }
        public DateTime HireDate { get; private set; }
        public double? Salary { get; private set; }
        public Employee? Subordinate { get; set; }

        public Employee(
            string name, string surname,
            string email, string phone,
            string address, int age,
            bool isStudying, bool isWorking,
            bool isRetired, Position empPosition, 
            DateTime hireDate, double? salary, 
            Retirement? retirementType = null
            ) : base(
                name, surname,
                email, phone,
                address, age,
                isStudying, isWorking,
                isRetired, retirementType
            )
        {
            EmpPosition = empPosition;
            HireDate = hireDate;
            Salary = salary;
            EmployeeId = _lastEmployeeId++;

            Employees.Add(this);
        }

        public Report CreateReport(string reportType, string content)
        {
            if (EmpPosition == Position.Manager)
            {
                return new Report(EmployeeId, reportType, content, DateTime.Now);
            }
            else
            {
                throw new InvalidOperationException("Only managers can create reports.");
            }
        }

        public static List<Employee> GetAllEmployees()
        {
            return Employees;
        }

        public override string ToString()
        {
            return base.ToString() + " position: " + EmpPosition;
        }

    }

}