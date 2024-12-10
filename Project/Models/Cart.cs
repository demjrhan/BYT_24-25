using Project.Entities;
using Project.Enum;
using Project.Features;
using Project.Utilities;

namespace Project.Models
{
    public class Cart
    {
        private static List<Cart> Instances = new List<Cart>();
        private int _customerId;
        private List<Tuple<Product, Promotion?>> _products = new List<Tuple<Product, Promotion?>>();
        private Customer _customer;
        public int CartId { get; set; }

        
        // Validation fixed, we need to check if customer exists to set customerId after object creation. If exists we can not initialize with new id.
        public int CustomerId
        {
            get => _customerId;
            set
            {
                if (Customer.Exists(value))
                    throw new ArgumentException($"Customer with ID {value} does exist.");
                _customerId = value;
            }
        }
        //public List<Tuple<Product, Promotion?>> Products
        //{
        //    get => _products;
        //    set
        //    {
        //        if (value == null)
        //            throw new ArgumentNullException(nameof(value), "Products list cannot be null.");

        //        foreach (var pair in value)
        //        {
        //            if (pair.Item1 == null)
        //                throw new ArgumentException("Product cannot be null.");

        //            if (pair.Item2 != null && !pair.Item1.Promotions.Contains(pair.Item2))
        //                throw new ArgumentException("The specified promotion is not valid for this product.");
        //        }

        //        _products = value;
        //    }
        //}

        //
        public Cart(Customer customer)
        {
            CartId = customer.CustomerId;
            CustomerId = customer.CustomerId;
            _customer = customer;

            Instances.Add(this);
        }
        
        public static IReadOnlyList<Cart> GetInstances()
        {
            return Instances.AsReadOnly();
        }
        
        public static void ClearInstances()
        {
            Instances.Clear();
        }

        public static bool Exists(Cart givenCart)
        {
            foreach (var cart in Instances)
            {
                if (cart == givenCart)
                    return true;
            }
            return false;
        }

        public void AddProduct(Product product, Promotion? promotion = null)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product), "Product cannot be null.");

            if (promotion != null && !product.ContainPromotion(promotion))
                throw new ArgumentException("The specified promotion is not valid for this product.");

            _products.Add(new Tuple<Product, Promotion?>(product, promotion));

            product.AddCart(this);
        }

        public void RemoveProduct(Product product, Promotion? promotion = null)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product), "Product cannot be null.");

            var productToRemove = _products
                .FirstOrDefault(pair => pair.Item1 == product && pair.Item2 == promotion) 
                ?? throw new ArgumentException("The specified product and promotion combination does not exist in the cart.");
            //foreach (var pair in _products)
            //{
            //    pair.Item1.RemoveCart(this);
            //    RemoveProduct(pair.Item1, pair?.Item2);
            //}
            productToRemove.Item1.RemoveCart(this);
            _products.Remove(productToRemove);
        }

        public double CalculateTotalSum()
        {
            double discount = Customer.GetDiscountPercentage(CustomerId);
            double totalSum = 0;

            foreach (var pair in _products)
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

            foreach (var pair in _products)
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

            foreach(var pair in _products)
            {
                pair.Item1.RemoveCart(this);
                RemoveProduct(pair.Item1, pair?.Item2);
            }

            return new Order(_customer, DateTime.Now, OrderStatus.Proccessing, amount, products);
        }

        public static void RemoveCart(Cart cart)
        {
            foreach (var pair in cart._products)
            {
                pair.Item1.RemoveCart(cart);
            }

            cart._products.Clear();

            Instances.Remove(cart);
        }
        
        public static void PrintInstances()
        {
            foreach (var cart in Instances)
            {
                Console.WriteLine(cart.ToString());
            }
        }

        public int Count()
        {
            return _products.Count;
        }

        public override string ToString()
        {
            string result = "";
            foreach (var pair in _products)
            {
                result += pair.Item1.ToString();
                result += "\n";
                if (pair.Item2 != null) result += pair.Item2.ToString();
            }
            return result;
        }

    }

}

