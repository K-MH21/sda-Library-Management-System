using System.Reflection.Metadata.Ecma335;

namespace LibraryManagementSystem
{
    public abstract class BaseClass
    {
        private static readonly Dictionary<ClassType, ClassTypeMetadata> _classTypeInfo =
            new Dictionary<ClassType, ClassTypeMetadata>
            {
                { ClassType.Book, new ClassTypeMetadata(0, 999) },
                { ClassType.User, new ClassTypeMetadata(1000, 1999) },
            };

        private int _id;
        public int Id
        {
            get => _id;
            private set
            {
                // Check Id limit to avoid wrong entity creation
                if (_classTypeInfo[EntityType].Counter >= _classTypeInfo[EntityType].LimitId)
                {
                    throw new InvalidOperationException("ID limit reached for " + EntityType);
                }
                // Increment and assign the ID counter
                ClassTypeMetadata typeMetadata = _classTypeInfo[EntityType];
                _id = ++typeMetadata.Counter;
                _classTypeInfo[EntityType] = typeMetadata;
            }
        }

        public ClassType EntityType { get; private set; }

        private DateTime _createdDate;
        public DateTime CreatedDate
        {
            get => _createdDate;
            private set
            {
                if (value.ToUniversalTime() <= DateTime.UtcNow.AddSeconds(-5))
                {
                    throw new ArgumentException("Date cannot be from the past");
                }

                _createdDate = value;
            }
        }

        public BaseClass(ClassType type, DateTime? createdDate = null)
        {
            // Assign Type (Needed in the Id setter)
            EntityType = type;

            // Generate Id (The assignment is to trigger the setter logic)
            Id = -1;

            // Assign Date
            createdDate ??= DateTime.Now;
            CreatedDate = createdDate.Value;
        }
    }
}
