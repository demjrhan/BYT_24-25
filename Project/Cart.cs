namespace Project
{
    public class Cart
    {
        public int CustomerId { get; set; }
        private List<Tuple<Product, Promotion>> Products { get; set; } = new List<Tuple<Product, Promotion>>();

        public Cart(int customerId)
        {
            CustomerId = customerId;
        }

        public void AddProduct(Product product, Promotion? promotion = null)
        {
            Products.Add(new Tuple<Product, Promotion>(product, promotion));
        }

        public void RemoveProduct(Product product, Promotion? promotion = null)
        {
            Tuple<Product, Promotion>? productToRemove = null;
            foreach (var pair in Products)
            {
                if (pair.Item1 == product && pair.Item2 == promotion)
                {
                    productToRemove = pair;
                    break;
                }
            }
            if (productToRemove != null) Products.Remove(productToRemove);
        }

        public double CalculateTotalSum()
        {
            Customer customer = Customer.Customers[CustomerId];
            double discount = customer.GetDiscountPercentage();
            double totalSum = 0;
            foreach (var pair in Products)
            {
                if (pair.Item2 != null && pair.Item1 != null) totalSum += pair.Item1.ApplyPromotion(pair.Item2);
                else if (pair.Item1 != null) totalSum += pair.Item1.Price;
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
            List<Product> products = new List<Product>();
            foreach (var pair in Products)
            {
                products.Add(pair.Item1);
            }
            Products = new List<Tuple<Product, Promotion>>();

            return new Order(CustomerId, DateTime.Now, "proccessing", amount, products);
        }

    }

}

