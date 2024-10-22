namespace Project
{
    public class Customer : Person
    {
        private static int _lastCustomerId = 0;
        public static List<Customer> Customers = new List<Customer>();

        public int CustomerId { get; private set; }
        public DateTime RegisterDate { get; private set; }
        public Cart Cart { get; set; }
        public Membership? Membership { get; set; }

        public Customer(
            string name, string surname,
            string email, string phone,
            string address, int age,
            bool isStudying, bool isWorking,
            bool isRetired, Retirement? retirementType = null
            ) : base(
                name, surname,
                email, phone,
                address, age,
                isStudying, isWorking,
                isRetired, retirementType
                )
        {
            RegisterDate = DateTime.Now;
            CustomerId = _lastCustomerId++;

            Cart = new Cart(CustomerId);
            Customers.Add(this);
        }

        public Order CreateOrder()
        {
            return Cart.ConvertToOrder();
        }

        public Payment CreatePayment(int orderId, string paymentMethod, double amount)
        {
            return new Payment(orderId, paymentMethod, amount);
        }

        public Review CreateReview(int rating, string comment)
        {
            return new Review(CustomerId, rating, comment);
        }

        public static List<Customer> GetAllCustomers()
        {
            return Customers;
        }

        public override string ToString()
        {
            return base.ToString() + " - Customer";
        }

        public static Customer GetCustomerWithId(int customerId)
        {
            foreach (var customer in Customers)
            {
                if (customer.CustomerId == customerId)
                {
                    return customer;
                }
            }

            return null;
        }
    }

}
