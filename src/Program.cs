using System.Runtime.InteropServices;

namespace LibraryManagementSystem
{
    internal class Program
    {
        private static void Main()
        {
            // Program.cs - You can also change these sample codes to adapt to your design
            var user1 = new User("Alice", new DateTime(2023, 1, 1));
            var user2 = new User("Bob", new DateTime(2023, 2, 1));
            var user3 = new User("Charlie", new DateTime(2023, 3, 1));
            var user4 = new User("David", new DateTime(2023, 4, 1));
            var user5 = new User("Eve", new DateTime(2024, 5, 1));
            var user6 = new User("Fiona", new DateTime(2024, 6, 1));
            var user7 = new User("George", new DateTime(2024, 7, 1));
            var user8 = new User("Hannah", new DateTime(2024, 8, 1));
            var user9 = new User("Ian");
            var user10 = new User("Julia");

            var book1 = new Book("The Great Gatsby", new DateTime(2023, 1, 1));
            var book2 = new Book("1984", new DateTime(2023, 2, 1));
            var book3 = new Book("To Kill a Mockingbird", new DateTime(2023, 3, 1));
            var book4 = new Book("The Catcher in the Rye", new DateTime(2023, 4, 1));
            var book5 = new Book("Pride and Prejudice", new DateTime(2023, 5, 1));
            var book6 = new Book("Wuthering Heights", new DateTime(2023, 6, 1));
            var book7 = new Book("Jane Eyre", new DateTime(2023, 7, 1));
            var book8 = new Book("Brave New World", new DateTime(2023, 8, 1));
            var book9 = new Book("Moby-Dick", new DateTime(2023, 9, 1));
            var book10 = new Book("War and Peace", new DateTime(2023, 10, 1));
            var book11 = new Book("Hamlet", new DateTime(2023, 11, 1));
            var book12 = new Book("Great Expectations", new DateTime(2023, 12, 1));
            var book13 = new Book("Ulysses", new DateTime(2024, 1, 1));
            var book14 = new Book("The Odyssey", new DateTime(2024, 2, 1));
            var book15 = new Book("The Divine Comedy", new DateTime(2024, 3, 1));
            var book16 = new Book("Crime and Punishment", new DateTime(2024, 4, 1));
            var book17 = new Book("The Brothers Karamazov", new DateTime(2024, 5, 1));
            var book18 = new Book("Don Quixote", new DateTime(2024, 6, 1));
            var book19 = new Book("The Iliad");
            var book20 = new Book("Anna Karenina");

            Library library = new Library();

            // Add users to the library
            library.AddUser(user1);
            library.AddUser(user2);
            library.AddUser(user3);
            library.AddUser(user4);
            library.AddUser(user5);
            library.AddUser(user6);
            library.AddUser(user7);
            library.AddUser(user8);
            library.AddUser(user9);
            library.AddUser(user10);

            // Add books to the library
            library.AddBook(book1);
            library.AddBook(book2);
            library.AddBook(book3);
            library.AddBook(book4);
            library.AddBook(book5);
            library.AddBook(book6);
            library.AddBook(book7);
            library.AddBook(book8);
            library.AddBook(book9);
            library.AddBook(book10);
            library.AddBook(book11);
            library.AddBook(book12);
            library.AddBook(book13);
            library.AddBook(book14);
            library.AddBook(book15);
            library.AddBook(book16);
            library.AddBook(book17);
            library.AddBook(book18);
            library.AddBook(book19);
            library.AddBook(book20);
            {
                // Test pagination for books

                // This need to be manually tested
                var booksPage1 = library.GetAllBooks(1, 2);
                Console.WriteLine($"Page 1, Book 1: {booksPage1[0].Title}");
                Console.WriteLine($"Page 1, Book 2: {booksPage1[1].Title}");

                var booksPage2 = library.GetAllBooks(2, 2);
                Console.WriteLine($"Page 2, Book 1: {booksPage2[0].Title}");

                // Test out-of-range page
                try
                {
                    var booksPage3 = library.GetAllBooks(123, 2);
                }
                catch (IndexOutOfRangeException e)
                {
                    Console.WriteLine(e.Message.Contains("Page number exceeds the number of"));
                }

                // Test empty list
                try
                {
                    new Library().GetAllUsers(1, 1);
                }
                catch (InvalidOperationException e)
                {
                    Console.WriteLine(e.Message.Contains("Empty"));
                }

                // Test invalid page size or number
                try
                {
                    var invalidBooksPage = library.GetAllBooks(0, 2);
                }
                catch (ArgumentOutOfRangeException e)
                {
                    Console.WriteLine(
                        e.Message.Contains("Invalid arguments, must be greater than 0")
                    );
                }
            }
            {
                // Test Find book/user by title/name
                Console.WriteLine(library.FindUserByName(user1.Name) == user1);
                Console.WriteLine(library.FindBookByTitle(book1.Title) == book1);
                try
                {
                    library.FindBookByTitle("");
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message.Contains("Empty"));
                }

                try
                {
                    library.FindUserByName("Mr. Vincent");
                }
                catch (KeyNotFoundException e)
                {
                    Console.WriteLine(e.Message.Contains("not found"));
                }
            }
            {
                // Test add and delete methods
                Library library1 = new Library();

                var newBook = new Book("b1");
                library1.AddBook(newBook);
                Console.WriteLine(library1.FindBookById(newBook.Id) == newBook);
                library1.DeleteBook(newBook);
                try
                {
                    library1.AddBook(newBook);
                }
                catch (InvalidOperationException e)
                {
                    Console.WriteLine(e.Message.Contains("in the library"));
                }
                try
                {
                    library1.FindBookByTitle(newBook.Title);
                }
                catch (KeyNotFoundException e)
                {
                    Console.WriteLine(e.Message.Contains("not found"));
                }

                var newUser = new User("u1");
                library1.AddUser(newUser);
                Console.WriteLine(library1.FindUserById(newUser.Id) == newUser);
                library1.DeleteUser(newUser);
                try
                {
                    library1.FindUserByName(newUser.Name);
                }
                catch (KeyNotFoundException e)
                {
                    Console.WriteLine(e.Message.Contains("not found"));
                }

                try
                {
                    library1.AddBook(null);
                }
                catch (ArgumentNullException e)
                {
                    Console.WriteLine(e.Message.Contains("Null"));
                }
            }
        }
    }
}
