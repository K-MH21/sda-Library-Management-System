namespace LibraryManagementSystem
{
    public class User : BaseClass
    {
        private string _name = string.Empty;
        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Name cannot be empty");
                }
                _name = value;
            }
        }

        public User(string name, DateTime? createdDate = null)
            : base(ClassType.User, createdDate)
        {
            Name = name;
        }
    }
}
