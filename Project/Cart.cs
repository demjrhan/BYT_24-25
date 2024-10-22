namespace Project
{
    public class Cart
    {
        public int CustomerId { get; set; }
        protected internal List<Tuple<Product, Promotion>> Products { get; set; } = new List<Tuple<Product, Promotion>>();

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
           
            if ( Products.Count == 0) return 0; 
            

            Customer customer = Customer.GetCustomerWithId(CustomerId);
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
                if (pair.Item1 is Book) Inventory.TotalBooksQuantity -= 1;
                else if (pair.Item1 is Accessory) Inventory.TotalAccessoriesQuantity -= 1;
            }
            Inventory.UpdateInventory();
            Products = new List<Tuple<Product, Promotion>>();

            return new Order(CustomerId, DateTime.Now, "proccessing", amount, products);
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

