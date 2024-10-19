namespace Project
{
    public class Cart
    {
        public int CustomerId { get; set; }
        private List<Product> Products { get; set; }

        public Cart(int customerId)
        {
            CustomerId = customerId;
        }

        public void AddProduct(Product product)
        {
            Products.Add(product);
        }

        public void RemoveProduct(Product product)
        {
            foreach (var each in Products)
            {
                if (each == product)
                {
                    Products.Remove(product);
                    break;
                }
            }
        }

        public double CalculateTotalSum()
        {
            Customer customer = Customer.Customers[CustomerId];
            double discount = customer.GetDiscountPercentage();
            double totalSum = 0;
            foreach (var each in Products)
            {
                totalSum += each.ApplyPromotion();
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
                products.Add(pair);
            }
            Products = new List<Product>();

            return new Order(CustomerId, DateTime.Now, "proccessing", amount, products);
        }

    }

}

