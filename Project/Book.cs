namespace Project
{
    public class Book : Product
    {
        public string Author { get; set; }
        public string Genre { get; set; }
        public int PublicationYear { get; set; }

        public Book(
            string title,
            double price, int stockQuantity,
            string author, string genre,
            int publicationYear
            ) : base(
                title,
                price, stockQuantity
                )
        {
            Author = author;
            Genre = genre;
            PublicationYear = publicationYear;
            Inventory.TotalBooksQuantity += StockQuantity;
            Inventory.UpdateInventory();
        }

        public override string ToString()
        {
            return base.ToString() + " Author: " + Author + " Genre: " + Genre + " Publication Year: " + PublicationYear;
        }
    }

}

