using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Project
{
    public enum Position
    {
        Manager,
        Assistant
    }

    public class Employee : Person
    {
        private static int _lastId = 0;
        private static List<Employee> Instances = [];

        //public static readonly string _verbose = "Employee";
        //public static readonly string _verbosePlural = "Employees";

        private DateTime _hireDate;
        private double? _salary;

        public int EmployeeId { get; private set; } = _lastId++;
        public Position EmpPosition { get; private set; }
        public DateTime HireDate
        {
            get => _hireDate;
            set
            {
                if (value > DateTime.Now)
                    throw new ArgumentException("Date cannot be set to a future time.", nameof(value));
                _hireDate = value;
            }
        }
        public double? Salary
        {
            get => _salary;
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Salary cannot be negative.");
                _salary = value;
            }
        }
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

            Instances.Add(this);
        }

        public Report CreateReport(ReportType reportType, string content)
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

        protected internal static List<Employee> GetInstances()
        {
            return Instances;
        }

        public override string ToString()
        {
            return base.ToString() + " position: " + EmpPosition;
        }
    }
}