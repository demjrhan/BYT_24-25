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
        private Employee? _mentor;
        private Employee? _subordinate;
        public int EmployeeId { get; private set; }
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

        public static IReadOnlyList<Employee> GetInstances()
        {
            return Instances.AsReadOnly();
        }


        public static void ClearInstances()
        {
            Instances.Clear();
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
            _subordinate = subordinate;
            subordinate._mentor = this;
            SubordinateId = subordinate.EmployeeId;
            subordinate.MentorId = EmployeeId;
        }
        public void RemoveSubordinate()
        {
            if (_subordinate != null)
            {
                _subordinate.MentorId = null;
                SubordinateId = null;
                _subordinate._mentor = null;
                _subordinate = null;
            }
        }
        public void AddMentor(Employee mentor) 
        {
            if (mentor == this) throw new Exception("Employee cannot be assigned as a mentor to himself");
            _mentor = mentor; 
            mentor._subordinate = this;
            MentorId = mentor.EmployeeId;
            mentor.SubordinateId = EmployeeId;
        }
        public void RemoveMentor()
        {
            if (_mentor != null)
            {
                _mentor.SubordinateId = null;
                MentorId = null;
                _mentor._subordinate = null;
                _mentor = null;
            }
        }
        public static void RemoveEmployee(Employee employee)
        {
            employee._mentor?.RemoveMentor();
            employee._subordinate?.RemoveSubordinate();
            Instances.Remove(employee);
        }
        public override string ToString()
        {
            return base.ToString() + " position: " + EmpPosition;
        }
    }
}