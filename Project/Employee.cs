namespace Project
{
    public class Employee : Person
    {
        private static int _lastId = 0;
        private static List<Employee> Instances = [];

        //public static readonly string _verbose = "Employee";
        //public static readonly string _verbosePlural = "Employees";

        private DateTime _hireDate;
        private double _salary;

        public int EmployeeId { get; private set; } = _lastId++;
        public int? SubordinateId { get; set; } 
        public int? MentorId { get; set; }
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
        public Employee? Subordinate { get; set; }
        public Employee? Mentor { get; set; }

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

        public void AddSubordinate(Employee subordinate)
        {
            if (subordinate == this) throw new Exception("Employee cannot be assigned as a subordinate to himself");
            Subordinate = subordinate;
            subordinate.Mentor = this;
            SubordinateId = subordinate.EmployeeId;
            subordinate.MentorId = EmployeeId;
        }

        public void RemoveSubordinate()
        {
            if (Subordinate != null)
            {
                Subordinate.MentorId = null;
                SubordinateId = null;
                Subordinate.Mentor = null;
                Subordinate = null;
            }
        }

        public void AddMentor(Employee mentor) 
        {
            if (mentor == this) throw new Exception("Employee cannot be assigned as a mentor to himself");
            Mentor = mentor; 
            mentor.Subordinate = this;
            MentorId = mentor.EmployeeId;
            mentor.SubordinateId = EmployeeId;
        }

        public void RemoveMentor()
        {
            if (Mentor != null)
            {
                Mentor.SubordinateId = null;
                MentorId = null;
                Mentor.Subordinate = null;
                Mentor = null;
            }
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

        public static void RemoveEmployee(Employee employee)
        {
            employee.Mentor?.RemoveMentor();
            employee.Subordinate?.RemoveSubordinate();
            Instances.Remove(employee);
        }

        public static void PrintInstances()
        {
            foreach (var i in Instances)
            {
                Console.WriteLine(i.ToString());
            }
        }

        public override string ToString()
        {
            return base.ToString() + " position: " + EmpPosition;
        }
    }
}