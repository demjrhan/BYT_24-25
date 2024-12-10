using Project.Entities;

namespace Project.Features
{
    public class Membership
    {
        private static int _lastId = 0;
        private static List<Membership> Instances = new List<Membership>();
        private Customer _customer;
        private int _customerId;
        private double _discountRate;
        public int MembershipId { get; private set; } = _lastId++;
        public bool Status { get; set; }
        
        public int CustomerId
        {
            get => _customerId;
            set
            {
                if (!Customer.Exists(value))
                    throw new ArgumentException($"Customer with ID {value} does not exist.");
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

        public Membership(Customer customer, bool status, double discountRate)
        {

            ValidateCustomerId(customer.CustomerId);
            ValidateDiscountRate(discountRate);

            _customer = customer;
            CustomerId = customer.CustomerId;
            Status = status;
            DiscountRate = discountRate;

            customer.SetMembership(this);

            Instances.Add(this);
        }

        public static void PrintInstances()
        {
            foreach (var membership in Instances)
            {
                Console.WriteLine(membership.ToString());
            }
        }
        public static IReadOnlyList<Membership> GetInstances()
        {
            return Instances.AsReadOnly();
        }
        
        public static void ClearInstances()
        {
            Instances.Clear();
        }

        public static bool Exists(Membership givenMembership)
        {
            foreach (var membership in Instances)
            {
                if (membership == givenMembership)
                    return true;
            }
            return false;
        }
        public static void RemoveMembership(Membership membership)
        {
            if (!Exists(membership))
            {
                throw new InvalidOperationException($"Membership does not exist in the collection.");
            }
            membership._customer.ResetMembership();

            Instances.Remove(membership);
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
