using Project;

namespace Project_Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            //Book.GetInstances().Clear();
            SerializeDeserialize.ResetInstances<Book>(typeof(Book));
        }

        [Test]
        public void Test_Extent()
        {
            var book1 = new Book("Harry Potter", 25, 300, "J.K.Rowling", "Fantasy", 2000);
            var book2 = new Book("Harry Potter 2", 25, 300, "J.K.Rowling", "Fantasy", 2002);

            Assert.That(Book.GetInstances().Count, Is.EqualTo(2), "Extent should contain 2 instances");
            Assert.That(book1, Is.EqualTo(Book.GetInstances()[0]), "Extent should contain the correct first instance");
            Assert.That(book2, Is.EqualTo(Book.GetInstances()[1]), "Extent should contain the correct second instance");
        }

        [Test]
        public void Test_EncapsulationModification()
        {
            var book = new Book("Harry Potter", 25, 300, "J.K.Rowling", "Fantasy", 2000);
            book.Author = "Doyle";

            Assert.That(book.Author, Is.EqualTo("Doyle"), "Author should be changed directly only within instance scope");
            Assert.That(Book.GetInstances()[0].Author, Is.EqualTo("Doyle"), "Extent should reflect changes made in the instance");
        }

        [Test]
        public void Test_Persistence()
        {
            var book1 = new Book("Harry Potter", 25, 300, "J.K.Rowling", "Fantasy", 2000);
            var book2 = new Book("Harry Potter 2", 25, 300, "J.K.Rowling", "Fantasy", 2002);
            string fileName = "test.json";

            try
            {
                SerializeDeserialize.SerializeToFile(fileName);
                SerializeDeserialize.ResetInstances<Book>(typeof(Book));
                SerializeDeserialize.DeserializeFromFile(fileName);

                Assert.That(Book.GetInstances().Count, Is.EqualTo(2), "Extent should contain 2 instances after loading.");
                Assert.That(book1.ToString(), Is.EqualTo(Book.GetInstances()[0].ToString()), "Extent should contain the correct first instance");
                Assert.That(book2.ToString(), Is.EqualTo(Book.GetInstances()[1].ToString()), "Extent should contain the correct second instance");
            }
            finally
            {
                if (File.Exists(fileName))
                    File.Delete(fileName);
            }
        }
    }
}