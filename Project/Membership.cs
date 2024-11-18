namespace Project
{
    public class Membership
    {
        private static int _lastId = 0;
        private static List<Membership> Instances = [];

        private int _customerId;
        private double _discountRate;

        public int MembershipId { get; private set; } = _lastId++;
        public int CustomerId
        {
            get => _customerId;
            set
            {
                if (!Customer.Exists(value))
                    throw new ArgumentException("Customer ID does not exist.");
                _customerId = value;
            }
        }
        public bool Status { get; set; }
        public double DiscountRate
        {
            get => _discountRate;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Discount Rate cannot be negative.");
                _discountRate = value;
            }
        }

        public Membership(int customerId, bool status, double discountRate)
        {
            CustomerId = customerId;
            Status = status;
            DiscountRate = discountRate;

            Instances.Add(this);
        }

        public static void GetInstances()
        {
            foreach (var i in Instances)
            {
                Console.WriteLine(i.ToString());
            }
        }
    }

}
