namespace LibraryManagementSystem
{
    public struct ClassTypeMetadata
    {
        public int BaseId { get; }
        public int LimitId { get; }
        public int Counter { get; set; }

        public ClassTypeMetadata(int baseId, int limitId)
        {
            BaseId = baseId;
            LimitId = limitId;
            Counter = baseId + 1;
        }
    }
}
