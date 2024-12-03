using Project.Utilities;

namespace Project.Models
{
    public class Book : Product
    {
        private static List<Book> Instances = new List<Book>();
        private string _author = null!;
        private string _genre = null!;
        private int _publicationYear;
        
        //public static readonly string _verbose = "Book";
        //public static readonly string _verbosePlural = "Books";
        
        
        public Book(
            string title, double price, 
            int stockQuantity, string author, 
            string genre, int publicationYear
            ) : base(
                title, price, 
                stockQuantity
                )
        {
            try
            {
                ValidateAuthor(author);
                ValidateGenre(genre);
                ValidatePublicationYear(publicationYear);

                Author = author;
                Genre = genre;
                PublicationYear = publicationYear;

                UpdateInventory();
                Instances.Add(this);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to initialize Book.", ex);
            }
        }
        public string Author 
        { 
            get => _author;
            set
            {
                ValidateAuthor(value);
                _author = value;
            }
        }

        private string Genre
        {
            get => _genre;
            set
            {
                ValidateGenre(value);
                _genre = value;
            }
        }

        private int PublicationYear
        {
            get => _publicationYear;
            set
            {
                ValidatePublicationYear(value);
                _publicationYear = value;
            }
        }
        public static IReadOnlyList<Book> GetInstances()
        {
            return Instances.AsReadOnly();
        }
        
        public static void ClearInstances()
        {
            Instances.Clear();
        }

        public static bool Exists(Book givenbBook)
        {
            foreach (var book in Instances)
            {
                if (book == givenbBook)
                    return true;
            }
            return false;
        }
        // Validation methods added seperately to maintain reusability and readability.
        private static void ValidateAuthor(string author)
        {
            if (string.IsNullOrWhiteSpace(author))
                throw new ArgumentException("Author cannot be null or empty.", nameof(author));
        }

        private static void ValidateGenre(string genre)
        {
            if (string.IsNullOrWhiteSpace(genre))
                throw new ArgumentException("Genre cannot be null or empty.", nameof(genre));
        }

        private static void ValidatePublicationYear(int year)
        {
            if (year < 1440) // First printed book
                throw new ArgumentOutOfRangeException(nameof(year), "Publication year cannot be before 1440.");
            if (year > DateTime.Now.Year)
                throw new ArgumentOutOfRangeException(nameof(year), "Publication year cannot be in the future.");
        }
        public new static void PrintInstances()
        {
            foreach (var book in Instances)
            {
                Console.WriteLine(book.ToString());
            }
        }

        public override string ToString()
        {
            return $"{base.ToString()}, Author: {Author}, Genre: {Genre}, Publication Year: {PublicationYear}";
        }
        
        public static int Count()
        {
            return Instances.Count;
        }
        private void UpdateInventory()
        {
            if (StockQuantity < 0)
                throw new InvalidOperationException("Stock quantity cannot be negative.");

            Inventory.TotalBooksQuantity += StockQuantity;
            Inventory.UpdateInventory();
        }
        public static Book? GetInstance(int id)
        {
            return Instances.Find(x => (x.ProductId == id));
        }
    }

}

