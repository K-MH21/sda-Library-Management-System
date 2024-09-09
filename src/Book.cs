namespace LibraryManagementSystem
{
    public class Book : BaseClass
    {
        private string _title = string.Empty;
        public string Title
        {
            get => _title;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Title cannot be empty");
                }
                _title = value;
            }
        }

        public Book(string title, DateTime? createdDate = null)
            : base(ClassType.Book, createdDate)
        {
            Title = title;
        }
    }
}
