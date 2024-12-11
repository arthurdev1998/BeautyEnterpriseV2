namespace BuildingBlocks.Core.Messages;

public abstract class Message<T>
{
    public string MessageType { get; protected set; }

    public T? AggregateId { get; protected set; }

    protected Message()
    {
        MessageType = GetType().Name;
    }
}