namespace Project
{
    public class Customer : Person
    {
        private static int _lastId = 0;
        private static List<Customer> Instances = [];

        //public static readonly string _verbose = "Customer";
        //public static readonly string _verbosePlural = "Customers";

        private DateTime _registerDate;
        private Cart? _cart;
        private Membership? _membership;

        public int CustomerId { get; private set; }
        public DateTime RegisterDate
        {
            get => _registerDate;
            set
            {
                if (value > DateTime.Now)
                    throw new ArgumentException("Date cannot be set to a future time.", nameof(value));
                _registerDate = value;
            }
        }
        public Cart? Cart
        {
            get => _cart;
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value), "Cart cannot be null.");
                if (value.CustomerId != CustomerId)
                    throw new ArgumentException("Cart customer ID must match the Customer's ID.");
                _cart = value;
            }
        }
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
            CustomerId = _lastId++;

            Cart = new Cart(CustomerId);

            Instances.Add(this);
        }

        public Order? CreateOrder()
        {
            if (Cart != null)
                return Cart.ConvertToOrder();
            return null;
        }

        public static Payment CreatePayment(int orderId, PaymentMethod paymentMethod, double amount)
        {
            return new Payment(orderId, paymentMethod, amount);
        }

        public Review CreateReview(int rating, string comment)
        {
            return new Review(CustomerId, rating, comment);
        }

        protected internal static List<Customer> GetInstances()
        {
            return Instances;
        }

        public override string ToString()
        {
            return base.ToString() + " - Customer";
        }
    }

}
