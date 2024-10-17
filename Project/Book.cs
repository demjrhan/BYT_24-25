namespace Project;

public class Book : Product
{
    public string Author { get; set; }
    public string Genre { get; set; }
    public int PublicationYear { get; set; }

    public Book(string productId, string title, double price, int stockQuantity, string author, string genre, int publicationYear)
        : base(productId, title, price, stockQuantity)
    {
        Author = author;
        Genre = genre;
        PublicationYear = publicationYear;
    }
}
