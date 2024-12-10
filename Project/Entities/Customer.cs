using Project.Enum;
using Project.Features;
using Project.Models;

namespace Project.Entities
{
    public class Customer : Person
    {
        private static int _lastId = 0;
        private static List<Customer> Instances = new List<Customer>();
        private Cart _cart = null!;
        private List<Review> _reviews = new List<Review>();
        private List<Product> _favorites = new List<Product>();
        private List<Order> _orders = new List<Order>();
        private List<Payment> _payments = new List<Payment>();
        private Membership? _membership;
        private DateTime _registerDate;
        public int CustomerId { get; private set; }
        //Add connection with Membership class
        
        // Taking 1940 as imaginary establishing date of our company. Person can not register before that time. -Demirhan
        private static readonly DateTime MinimumRegisterDate = new DateTime(1940, 1, 1);

        public DateTime RegisterDate
        {
            get => _registerDate;
            set
            {
                if (value > DateTime.Now)
                    throw new ArgumentException("Date cannot be set to a future time.", nameof(value));

                if (value < MinimumRegisterDate)
                    throw new ArgumentException($"Date cannot be earlier than {MinimumRegisterDate.ToShortDateString()}.", nameof(value));

                _registerDate = value;
            }
        }

        public Cart Cart
        {
            get => _cart;
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value), "Cart cannot be null.");
                // Added extra logic in case of cart already assigned to this customer. -Demirhan
                if (_cart == value)
                    throw new InvalidOperationException("The same cart is already assigned.");
               
                if (value.CustomerId != CustomerId)
                    throw new InvalidOperationException("This cart is already assigned to another customer.");

                _cart = value;
            }
        }
        public Customer(
            string name, string surname,
            string email, string phone,
            string address, int age,
            bool isStudying, bool isWorking,
            bool isRetired, RetirementType? retirementType = null
        ) : base(
            name, surname,
            email, phone,
            address, age,
            isStudying, isWorking,
            isRetired, retirementType
        )
        {
            try
            {
                RegisterDate = DateTime.Now;
                CustomerId = _lastId++;
                Cart = new Cart(this);
                Instances.Add(this);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to initialize Customer.", ex);
            }

        }

        public static IReadOnlyList<Customer> GetInstances()
        {
            return Instances.AsReadOnly();
        }


        public static void ClearInstances()
        {
            Instances.Clear();
        }
        public Order? CreateOrder()
        {
            /*Removed if Cart is null before converting the order.
            Cart can not be null since we are creating and initializing in the constructor. We also check it in setter.-Demirhan */ 
            return Cart.ConvertToOrder();
        }

        //public Review CreateReview(int rating, Product product, string? comment)
        //{
        //    return product.AddReview(this, rating, comment);
        //}

        public void AddReview(Review review)
        {
            _reviews.Add(review);
        }

        public static void PrintInstances()
        {
            foreach (var customer in Instances)
            {
                Console.WriteLine(customer.ToString());
            }
        }

        public static bool Exists(int id)
        {
            /*Code refactoring :  instead of checking by index in Instances list we are checking each instance one by one if its matching.
            If so returning true otherwise false. -Demirhan */
            foreach (var customer in Instances)
            {
                if (customer.CustomerId == id)
                    return true;
            }
            return false;
        }
    
        
        // Since we are checking already if customer exists in GetCustomerWithId method, there is no reason to second check. -Demirhan
        // If customer does not exist we will face with error rather than 0. -Demirhan
        public static double GetDiscountPercentage(int customerId)
        {
            Customer customer = GetCustomerWithId(customerId);
            return customer.GetDiscountPercentage();
        }

        public void SetMembership(Membership membership)
        {
            _membership = membership;
        }

        public void ResetMembership()
        {
            _membership = null;
        }
        
        public static void RemoveCustomer(Customer customer)
        {
            foreach (var product in customer._favorites)
            {
                product.RemoveCustomerFromFavorites(customer);
            }
            customer._favorites.Clear();
            foreach (var order in customer._orders)
            {
                Order.RemoveOrder(order);
            }
            customer._orders.Clear();
            if (customer._membership != null)
            {
                Membership.RemoveMembership(customer._membership);
            }
            Cart.RemoveCart(customer.Cart);
            Instances.Remove(customer);
        }
        
        // Created getting customer with id method to apply reusability in code. -Demirhan
        private static Customer GetCustomerWithId(int customerId)
        {
            Customer? customer = Instances.Find(x => x.CustomerId == customerId);
            if (customer == null)
                throw new InvalidOperationException($"Customer with ID {customerId} does not exist.");
            return customer;
        }

        public void RemoveReview(Review review)
        {
            _reviews.Remove(review);
        }

        public void AddToFavorites(Product product)
        {
            // probably need to add check if product already there
            product.AddCustomerToFavorites(this);
            _favorites.Add(product);
        }

        public void RemoveFromFavorites(Product product)
        {
            product.RemoveCustomerFromFavorites(this);
            _favorites.Remove(product);
        }

        public void AddOrder(Order order)
        {
            _orders.Add(order);
        }

        public void RemoveOrder(Order order)
        {
            _orders.Remove(order);
        }

        public void AddPayment(Payment payment)
        {
            _payments.Add(payment);
        }

        public void RemovePayment(Payment payment)
        {
            _payments.Remove(payment);
        }

        public bool IsMember()
        {
            return _membership != null;
        }

        public override string ToString()
        {
            return base.ToString() + " - Customer";
        }
    }
}