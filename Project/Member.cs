namespace Project
{
    public class Membership
    {
        private static int _lastMembershipId = 0;

        public int MembershipId { get; private set; }
        public int CustomerId { get; set; }
        public bool Status { get; set; }
        public double DiscountRate { get; set; }

        public Membership(int customerId, bool status, double discountRate)
        {
            CustomerId = customerId;
            Status = status;
            DiscountRate = discountRate;
            MembershipId = _lastMembershipId++;
        }
    }

}
