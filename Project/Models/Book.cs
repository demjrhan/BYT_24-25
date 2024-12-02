using Project.Utilities;

namespace Project.Models
{
    public class Book : Product
    {
        private static List<Book> Instances = [];

        //public static readonly string _verbose = "Book";
        //public static readonly string _verbosePlural = "Books";

        private string _author = null!;
        private string _genre = null!;
        private int _publicationYear;

        public string Author 
        { 
            get => _author;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Author cannot be null or empty.");
                _author = value;
            }
        }
        public string Genre
        {
            get => _genre;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Genre cannot be null or empty.");
                _genre = value;
            }
        }
        public int PublicationYear
        {
            get => _publicationYear;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Stock quantity cannot be negative.");
                _publicationYear = value;
            }
        }
        public Book(
            string title, double price, 
            int stockQuantity, string author, 
            string genre, int publicationYear
            ) : base(
                title, price, 
                stockQuantity
                )
        {
            Author = author;
            Genre = genre;
            PublicationYear = publicationYear;

            Inventory.TotalBooksQuantity += StockQuantity;
            Inventory.UpdateInventory();

            Instances.Add(this);
        }

        public static new void PrintInstances()
        {
            foreach (var i in Instances)
            {
                Console.WriteLine(i.ToString());
            }
        }

        public override string ToString()
        {
            return base.ToString() + " Author: " + Author + " Genre: " + Genre + " Publication Year: " + PublicationYear;
        }

        //Methods for tests
        public static int Count()
        {
            return Instances.Count;
        }

        public static Book? GetInstance(int id)
        {
            return Instances.Find(x => (x.ProductId == id));
        }
    }

}

