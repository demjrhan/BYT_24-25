namespace Project
{
    public class Customer : Person
    {
        private static int _lastCustomerId = 0;
        internal static List<Customer> Customers = new List<Customer>();

        public int CustomerId { get; private set; }
        public DateTime RegisterDate { get; private set; }
        public Cart Cart { get; set; }

        public Customer(
            string name, string surname,
            string email, string phone,
            string address, int age,
            bool isStudying, bool isWorking,
            bool isRetired, Retirement retirementType
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

        public List<Customer> GetAllCustomers()
        {
            return Customers;
        }
    }

}
