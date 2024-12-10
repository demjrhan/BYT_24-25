using Project.Enum;
using Project.Interfaces;

namespace Project.Entities
{
    public abstract class Person : IYoung, IAdult, IRetired
    {
        private static readonly double _discountStudying = 15.0;
        private static readonly double _discountWorking = 5.0;
        //private static readonly double _dicsountRetired = 0;

        private string _name = null!;
        private string _surname = null!;
        private string? _email;
        private string? _phone;
        private string? _address;
        private int _age;
        
        public bool IsStudying { get; set; }
        public bool IsWorking { get; set; }
        public bool IsRetired { get; set; }
        public RetirementType? RetirementType { get; set; }

        public Person(string name, string surname,
            string email, string phone,
            string address, int age,
            bool isStudying, bool isWorking,
            bool isRetired, RetirementType? retirementType = null)
        {
            Name = name;
            Surname = surname;
            Email = email;
            Phone = phone;
            Address = address;
            Age = age;
            IsStudying = isStudying;
            IsWorking = isWorking;
            
            ValidateRetirement(isRetired, retirementType);
            
            IsRetired = isRetired;
            RetirementType = retirementType;
        }

        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Name cannot be null or empty.");
                _name = value;
            }
        }
        public string Surname
        {
            get => _surname;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Surname cannot be null or empty.");
                _surname = value;
            }
        }
        
        // Instead of checking if mail is "" applied email validation.
        public string? Email
        {
            get => _email;
            set
            {
                if (value != null && !IsValidEmail(value))
                    throw new ArgumentException("Invalid email format.", nameof(value));
                _email = value;
            }
        }
        
        // Checking phone number must be at least 7 digit rather than checking if its empty.
        public string? Phone
        {
            get => _phone;
            set
            {
                if (value != null && value.Length < 7)
                    throw new ArgumentException("Phone number must have at least 7 digits.");
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
        
        // Extra validation of checking if age is greater than 110. I believe more than 110 is really extraordinary...
        public int Age
        {
            get => _age;
            set
            {
                if (value < 0 || value > 110)
                    throw new ArgumentException(nameof(value), "Age must be between 0 and 110.");
                _age = value;
            }
        }
        
        public double GetDiscountPercentage()
        {
            if (IsStudying) return _discountStudying;
            if (IsWorking) return _discountWorking;
            if (IsRetired) return RetirementType == Enum.RetirementType.Other ? 5.0 : 15.0;
            return 0;
        }

        // Validation methods added seperately to maintain reusability and readability.
        private static bool IsValidEmail(string email)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(
                email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                System.Text.RegularExpressions.RegexOptions.IgnoreCase
            );
        }
        
        private static void ValidateRetirement(bool isRetired, RetirementType? retirementType)
        {
            if (!isRetired && retirementType != null)
                throw new ArgumentException("RetirementType should be null if the customer is not retired.", nameof(retirementType));
        }
        public override string ToString()
        {
            return "Name: " + Name + "Surname " + Surname;
        }

    }

}