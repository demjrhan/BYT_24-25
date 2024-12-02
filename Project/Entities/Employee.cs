using Project.Enum;
using Project.Features;

namespace Project.Entities
{
    public class Employee : Person
    {
        private static int _lastId = 0;
        private static List<Employee> Instances = new List<Employee>();
        private DateTime _hireDate;
        private double _salary;
        public int EmployeeId { get; private set; } = _lastId++;
        public Position EmpPosition { get; private set; }
        public Employee? Subordinate { get; set; }
        //public static readonly string _verbose = "Employee";
        //public static readonly string _verbosePlural = "Employees";
        

        public Employee(
            string name, string surname,
            string email, string phone,
            string address, int age,
            bool isStudying, bool isWorking,
            bool isRetired, Position empPosition, 
            DateTime hireDate, double salary, 
            RetirementType? retirementType = null
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
        public double Salary
        {
            get => _salary;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Salary cannot be negative.");
                _salary = value;
            }
        }
        
        // Added extra validation InvalidOperationException in case of something goes wrong. - Demirhan
        public Report CreateReport(ReportType reportType, string content)
        {
            try
            {
                if (EmpPosition != Position.Manager)
                    throw new InvalidOperationException("Only managers can create reports.");

                return new Report(EmployeeId, reportType, content, DateTime.Now);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to create a report.", ex);
            }
        }


        public static void PrintInstances()
        {
            foreach (var employee in Instances)
            {
                Console.WriteLine(employee.ToString());
            }
        }

        public override string ToString()
        {
            return base.ToString() + " position: " + EmpPosition;
        }
    }
}