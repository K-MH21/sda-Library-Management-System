public struct ClassTypeMetadata
{
    public readonly int BaseId { get; }
    public readonly int LimitId { get; }
    public int Counter { get; set; }

    public ClassTypeMetadata(int baseId, int limitId)
    {
        BaseId = baseId;
        LimitId = limitId;
        Counter = baseId + 1;
    }
}
