using Project.Entities;

namespace Project.Features
{
    public class Membership
    {
        private static int _lastId = 0;
        private static List<Membership> Instances = new List<Membership>();
        private int _customerId;
        private double _discountRate;
        public int MembershipId { get; private set; } = _lastId++;
        public bool Status { get; set; }
        
        public Membership(int customerId, bool status, double discountRate)
        {
            
            ValidateCustomerId(customerId);
            ValidateDiscountRate(discountRate);
            
            
            CustomerId = customerId;
            Status = status;
            DiscountRate = discountRate;
            Instances.Add(this);
        }
        public int CustomerId
        {
            get => _customerId;
            set
            {
                if (Customer.Exists(value))
                    throw new ArgumentException($"Customer with ID {value} exist.");
                _customerId = value;
            }
        }
        
        public double DiscountRate
        {
            get => _discountRate;
            set
            {
                ValidateDiscountRate(value);
                _discountRate = value;
            }
        }
        public static void PrintInstances()
        {
            foreach (var membership in Instances)
            {
                Console.WriteLine(membership.ToString());
            }
        }
        
        // Validation methods added seperately to maintain reusability and readability.
        private static void ValidateCustomerId(int customerId)
        {
            if (!Customer.Exists(customerId))
                throw new ArgumentException($"Customer with ID {customerId} does not exist.");
        }
        private static void ValidateDiscountRate(double discountRate)
        {
            if (discountRate < 0)
                throw new ArgumentOutOfRangeException(nameof(discountRate), "Discount Rate cannot be negative.");
        }
        
        public override string ToString()
        {
            return $"MembershipId: {MembershipId}, CustomerId: {CustomerId}, Status: {(Status ? "Active" : "Inactive")}, DiscountRate: {DiscountRate}%";
        }
    }

}
