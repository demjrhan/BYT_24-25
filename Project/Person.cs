namespace Project
{
    public abstract class Person : IYoung, IAdult, IRetired
    {
        private static readonly double _discountStudying = 15;
        private static readonly double _discountWorking = 5;
        //private static readonly double _dicsountRetired = 0;

        private string? _name;
        private string? _surname;
        private string? _email;
        private string? _phone;
        private string? _address;
        private int _age;

        public string? Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Name cannot be null or empty.");
                _name = value;
            }
        }
        public string? Surname
        {
            get => _surname;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Surname cannot be null or empty.");
                _surname = value;
            }
        }
        public string? Email
        {
            get => _email;
            set
            {
                if (value != null && value.Equals(""))
                    throw new ArgumentException("Email cannot be empty.");
                _email = value;
            }
        }
        public string? Phone
        {
            get => _phone;
            set
            {
                if (value != null && value.Equals(""))
                    throw new ArgumentException("Phone cannot be empty.");
                _phone = value;
            }
        }
        public string? Address
        {
            get => _address;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Address cannot be null or empty.");
                _address = value;
            }
        }
        public int Age
        {
            get => _age;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Age cannot be negative.");
                _age = value;
            }
        }
        public bool IsStudying { get; set; }
        public bool IsWorking { get; set; }
        public bool IsRetired { get; set; }
        public Retirement? RetirementType { get; set; }

        public Person(string name, string surname,
            string email, string phone,
            string address, int age,
            bool isStudying, bool isWorking,
            bool isRetired, Retirement? retirementType = null)
        {
            Name = name;
            Surname = surname;
            Email = email;
            Phone = phone;
            Address = address;
            Age = age;
            IsStudying = isStudying;
            IsWorking = isWorking;
            IsRetired = isRetired;
            RetirementType = retirementType;
        }

        public double GetDiscountPercentage()
        {
            if (IsStudying) return _discountStudying;
            else if (IsWorking) return _discountWorking;
            else if (IsRetired) return RetirementType == Retirement.Other ? 5.0 : 15.0;

            return 0;
        }

        public override string ToString()
        {
            return "Name: " + Name;
        }

    }

}