namespace Project
{
    public class Cart
    {
        private static List<Cart> Instances = [];

        private int _customerId;
        private List<Tuple<Product, Promotion?>> _products = [];
        private Customer Customer { get; set; }

        public int CartId { get; set; }
        public int CustomerId
        {
            get => _customerId;
            set
            {
                //if (!Customer.GetInstances().Exists(c => c.CustomerId == value))
                //    throw new ArgumentException("Customer ID does not exist.");
                _customerId = value;
            }
        }
        public List<Tuple<Product, Promotion?>> Products
        {
            get => _products;
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value), "Products list cannot be null.");

                foreach (var pair in value)
                {
                    if (pair.Item1 == null)
                        throw new ArgumentException("Product cannot be null.");

                    if (pair.Item2 != null && !pair.Item1.Promotions.Contains(pair.Item2))
                        throw new ArgumentException("The specified promotion is not valid for this product.");
                }

                _products = value;
            }
        }

        public Cart(Customer customer)
        {
            CartId = customer.CustomerId;
            CustomerId = customer.CustomerId;
            Customer = customer;

            Instances.Add(this);
        }

        public void AddProduct(Product product, Promotion? promotion = null)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product), "Product cannot be null.");

            if (promotion != null && !product.Promotions.Contains(promotion))
                throw new ArgumentException("The specified promotion is not valid for this product.");

            _products.Add(new Tuple<Product, Promotion?>(product, promotion));
        }

        public void RemoveProduct(Product product, Promotion? promotion = null)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product), "Product cannot be null.");

            var productToRemove = _products
                .FirstOrDefault(pair => pair.Item1 == product && pair.Item2 == promotion) 
                ?? throw new ArgumentException("The specified product and promotion combination does not exist in the cart.");
            _products.Remove(productToRemove);
        }

        public double CalculateTotalSum()
        {
            double discount = Customer.GetDiscountPercentage(CustomerId);
            double totalSum = 0;

            foreach (var pair in Products)
            {
                totalSum += pair.Item2 != null ? pair.Item1.ApplyPromotion(pair.Item2) : pair.Item1.Price;
            }

            if (discount > 0)
            {
                totalSum -= totalSum * (discount / 100);
            }

            return totalSum;
        }

        public Order ConvertToOrder()
        {
            double amount = CalculateTotalSum();
            List<Product> products = [];

            foreach (var pair in Products)
            {
                products.Add(pair.Item1);

                if (pair.Item1 is Book)
                {
                    if (Inventory.TotalBooksQuantity <= 0)
                        throw new InvalidOperationException("Not enough books in stock.");
                    Inventory.TotalBooksQuantity -= 1;
                }
                else if (pair.Item1 is Accessory)
                {
                    if (Inventory.TotalAccessoriesQuantity <= 0)
                        throw new InvalidOperationException("Not enough accessories in stock.");
                    Inventory.TotalAccessoriesQuantity -= 1;
                }
            }

            Inventory.UpdateInventory();

            _products.Clear();

            return new Order(CustomerId, DateTime.Now, OrderStatus.Proccessing, amount, products);
        }

        public static void RemoveCart(Cart cart)
        {
            Instances.Remove(cart);
        }

        public static void PrintInstances()
        {
            foreach (var i in Instances)
            {
                Console.WriteLine(i.ToString());
            }
        }

        public override string ToString()
        {
            string result = "";
            foreach (var pair in Products)
            {
                result += pair.Item1.ToString();
                result += "\n";
                if (pair.Item2 != null) result += pair.Item2.ToString();
            }
            return result;
        }

    }

}

