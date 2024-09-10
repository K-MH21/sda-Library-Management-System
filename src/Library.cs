using System.ComponentModel.DataAnnotations;
using System.Net;
using Microsoft.VisualBasic.FileIO;

namespace LibraryManagementSystem
{
    public class Library
    {
        private List<User> usersList = new List<User>();
        private List<Book> booksList = new List<Book>();

        private INotificationService _notificationService;

        public List<Book> GetAllBooks(int pageNumber, int pageSize) =>
            GetAllEntity(pageNumber, pageSize, booksList);

        public List<User> GetAllUsers(int pageNumber, int pageSize) =>
            GetAllEntity(pageNumber, pageSize, usersList);

        private List<T> GetAllEntity<T>(int pageNumber, int pageSize, List<T> entityList)
            where T : BaseClass
        {
            if (pageNumber < 1 || pageSize < 1)
                throw new ArgumentOutOfRangeException("Invalid arguments, must be greater than 0");
            if (!entityList.Any())
                throw new InvalidOperationException("Empty list");
            if (pageNumber > entityList.Count)
                throw new IndexOutOfRangeException(
                    $"Page number exceeds the number of {entityList.First().EntityType.ToString()}"
                );
            return entityList.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        }

        public Book FindBookByTitle(string title) =>
            FindEntityByString<Book>(title, ClassType.Book);

        public User FindUserByName(string name) => FindEntityByString<User>(name, ClassType.User);

        private T FindEntityByString<T>(string str, ClassType type)
            where T : BaseClass
        {
            if (string.IsNullOrEmpty(str))
                throw new ArgumentException("Empty input");
            T? entity;
            switch (type)
            {
                case ClassType.Book:
                    entity = booksList.Find(e => e.Title.Equals(str)) as T;
                    break;
                case ClassType.User:
                    entity = usersList.Find(e => e.Name.Equals(str)) as T;
                    break;
                default:
                    throw new InvalidOperationException("Unsupported class type");
            }
            if (entity == null)
                throw new KeyNotFoundException("Item not found");
            return entity;
        }

        public void AddBook(Book book) => AddEntity(book);

        public void AddUser(User user) => AddEntity(user);

        private void AddEntity(BaseClass entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Null object");
            }
            switch (entity.EntityType)
            {
                case ClassType.Book:
                    Book book = entity as Book;
                    if (booksList.Any(e => e.Title.Equals(book.Title)))
                    {
                        string bookFailMessage = $"We encountered an issue adding {book.Title}.";
                        _notificationService.SendNotificationOnFailure(
                            bookFailMessage,
                            new DataTypeAttribute(DataType.Text)
                        );
                        throw new InvalidOperationException(
                            $"{book.Title} is already in the library"
                        );
                    }
                    booksList.Add(book);
                    string bookSeccessMessage =
                        $"A new book titled {book.Title} has been added to the Library.";
                    _notificationService.SendNotificationOnSuccess(
                        bookSeccessMessage,
                        new DataTypeAttribute(DataType.Text)
                    );
                    break;
                case ClassType.User:
                    User user = entity as User;
                    if (usersList.Any(e => e.Name.Equals(user.Name)))
                    {
                        string userFailMessage =
                            $"We encountered an issue creating account for {user.Name}.";
                        _notificationService.SendNotificationOnFailure(
                            userFailMessage,
                            new DataTypeAttribute(DataType.Text)
                        );
                        throw new InvalidOperationException(
                            $"{user.Name} is already in the library"
                        );
                    }
                    usersList.Add(user);
                    string userSeccessMessage = $"Welcome {user.Name}!";
                    _notificationService.SendNotificationOnSuccess(
                        userSeccessMessage,
                        new DataTypeAttribute(DataType.Text)
                    );
                    break;
                default:
                    throw new InvalidOperationException("Unsupported class type");
            }
        }

        // Made them into seperate methods (delete and search by ID) because it is very useful
        // and it could be used later in the third level.
        public Book FindBookById(int id)
        {
            var book = booksList.Find(e => e.Id == id);
            if (book == null)
                throw new KeyNotFoundException($"Book with ID {id} not found");
            return book;
        }

        public User FindUserById(int id)
        {
            var user = usersList.Find(e => e.Id == id);
            if (user == null)
                throw new KeyNotFoundException($"User with ID {id} not found");
            return user;
        }

        public void DeleteBookById(int id) => DeleteBook(FindBookById(id));

        public void DeleteUserById(int id) => DeleteUser(FindUserById(id));

        // These two methods beloew were implemented first because I didn't see the "by id".
        public void DeleteBook(Book book) => DeleteEntity(book);

        public void DeleteUser(User user) => DeleteEntity(user);

        private void DeleteEntity<T>(T entity)
            where T : BaseClass
        {
            if (entity == null)
                throw new ArgumentNullException("Null object");

            switch (entity.EntityType)
            {
                case ClassType.Book:
                    var book = entity as Book;
                    if (!booksList.Remove(book))
                    {
                        string bookFailMessage = $"We encountered an issue deleting {book.Title}.";
                        _notificationService.SendNotificationOnFailure(
                            bookFailMessage,
                            new DataTypeAttribute(DataType.Text)
                        );
                        throw new KeyNotFoundException($"{book.Title} not found in the library");
                    }
                    string bookSeccessMessage = $"Book titled {book.Title} has been deleted.";
                    _notificationService.SendNotificationOnSuccess(
                        bookSeccessMessage,
                        new DataTypeAttribute(DataType.Text)
                    );
                    break;
                case ClassType.User:
                    var user = entity as User;
                    if (!usersList.Remove(user))
                    {
                        string userFailMessage =
                            $"We encountered an issue creating account for {user.Name}.";
                        _notificationService.SendNotificationOnFailure(
                            userFailMessage,
                            new DataTypeAttribute(DataType.Text)
                        );
                        throw new KeyNotFoundException($"{user.Name} not found in the library");
                    }
                    string userSeccessMessage = $"Goodbuy {user.Name} 🥲";
                    _notificationService.SendNotificationOnSuccess(
                        userSeccessMessage,
                        new DataTypeAttribute(DataType.Text)
                    );
                    break;
                default:
                    throw new InvalidOperationException("Unsupported class type");
            }
        }

        public Library(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }
    }
}
