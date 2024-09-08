public abstract class BaseClass
{
    private static Dictionary<ClassType, ClassTypeMetadata> _classTypeInfo = new Dictionary<
        ClassType,
        ClassTypeMetadata
    >
    {
        { ClassType.Book, new ClassTypeMetadata(0, 999) },
        { ClassType.User, new ClassTypeMetadata(1000, 1999) },
    };
    protected readonly int _id;

    public int ID { get; }

    private ClassType _classType;

    public ClassType Type { get; }

    private DateTime _createdDate;

    public DateTime CreatedDate
    {
        get => _createdDate;
        set
        {
            if (
                DateTime.Compare(
                    value.ToUniversalTime(),
                    DateTime.Now.ToUniversalTime().AddMilliseconds(-100)
                ) <= 0
            )
            {
                throw new ArgumentException("Date cannot be from the past");
            }

            _createdDate = value;
        }
    }

    public BaseClass(ClassType type, DateTime? createdDate)
    {
        if (_classTypeInfo[type].Counter >= _classTypeInfo[type].LimitId)
        {
            throw new InvalidOperationException("ID limit reached for " + type);
        }
        ClassTypeMetadata typeMetadate = _classTypeInfo[type];
        _id = ++typeMetadate.Counter;
        _classTypeInfo[type] = typeMetadate;
        if (createdDate == null)
        {
            createdDate = DateTime.Now;
        }
        CreatedDate = (DateTime)createdDate;
    }

    public BaseClass(ClassType type)
        : this(type, DateTime.Now) { }
}
