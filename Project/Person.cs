namespace Project
{
    public abstract class Person : IYoung, IAdult, IRetired
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
        public bool IsStudying { get; set; }
        public bool IsWorking { get; set; }
        public bool IsRetired { get; set; }
        public Retirement? RetirementType { get; set; }

        protected Person(
            string name, string surname,
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
            if (IsStudying) return 15.0;
            else if (IsWorking) return 5.0;
            else if (IsRetired) return RetirementType == Retirement.Other ? 5.0 : 15.0;

            return 0;
        }

        public override string ToString()
        {
            return "Name: " + Name + "Surname " + Surname;
        }

    }

}