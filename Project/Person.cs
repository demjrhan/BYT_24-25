using Project;

public abstract class Person
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }

        protected static List<Person> personExtent = new List<Person>();

        
        private CustomerRole? _customerRole;
        private EmployeeRole? _employeeRole;

        protected Person(string name, string surname, string email, string phone, string address, int age)
        {
            Name = name;
            Surname = surname;
            Email = email;
            Phone = phone;
            Address = address;
            Age = age;

            personExtent.Add(this);
        }

        public static IReadOnlyList<Person> GetAllPersons() => personExtent.AsReadOnly();
        public bool IsCustomer() => _customerRole != null;
        public bool IsEmployee() => _employeeRole != null;
        public void AddCustomerRole()
        {
            if (_customerRole == null)
                _customerRole = new CustomerRole();
        }
        public void AddEmployeeRole(string position, DateTime hireDate, double? salary = null)
        {
            if (_employeeRole == null)
                _employeeRole = new EmployeeRole(position, hireDate, salary);
        }

        public Payment CreatePayment(string paymentMethod, double amount)
        {
            if (_customerRole != null)
            {
                return _customerRole.CreatePayment(paymentMethod, amount);
            }
            else
            {
                throw new InvalidOperationException("This person is not a customer.");
            }
        }

        public Review CreateReview(int rating, string comment)
        {
            if (_customerRole != null)
            {
                return _customerRole.CreateReview(rating, comment);
            }
            else
            {
                throw new InvalidOperationException("This person is not a customer.");
            }
        }

        public Report CreateReport(string reportType, string content)
        {
            if (_employeeRole != null)
            {
                return _employeeRole.CreateReport(reportType, content);
            }
            else
            {
                throw new InvalidOperationException("This person is not an employee.");
            }
        }

        public abstract double GetDiscountPercentage();

        public void DisplayRoles()
        {
            Console.WriteLine($"{Name} {Surname} is {(IsCustomer() ? "a customer" : "")} {(IsEmployee() ? "and an employee" : "")}");
        }
    }