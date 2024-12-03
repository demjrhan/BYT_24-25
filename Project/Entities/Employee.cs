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
        public int EmployeeId { get; private set; }
        public int? SubordinateId { get; set; } 
        public int? MentorId { get; set; }
        public Employee? Mentor { get; set; }
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
            EmployeeId = _lastId++;
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
        public static void RemoveEmployee(Employee employee)
        {
            employee.Mentor?.RemoveMentor();
            employee.Subordinate?.RemoveSubordinate();
            Instances.Remove(employee);
        }
        public override string ToString()
        {
            return base.ToString() + " position: " + EmpPosition;
        }
    }
}