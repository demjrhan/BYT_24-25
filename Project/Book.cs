﻿namespace Project
{
    public class Book : Product
    {
        public static readonly string _verbose = "book";
        public static readonly string _verbosePlural = "books";
        public string ProductClass { get; set; } = typeof(Book).Name;
        public string Author { get; set; }
        public string Genre { get; set; }
        public int PublicationYear { get; set; }
        public static List<Book> Products = [];

        static Book()
        {
            Types.Add(typeof(Book));
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
            Products.Add(this);
            Inventory.TotalBooksQuantity += StockQuantity;
            Inventory.UpdateInventory();
        }

        public override string ToString()
        {
            return base.ToString() + " Author: " + Author + " Genre: " + Genre + " Publication Year: " + PublicationYear;
        }
    }

}

