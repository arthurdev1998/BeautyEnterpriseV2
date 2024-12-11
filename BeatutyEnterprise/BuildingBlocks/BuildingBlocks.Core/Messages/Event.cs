using MediatR;

namespace BuildingBlocks.Core.Messages;

public class Event<T> : Message<T>, INotification
{
    public DateTime Timestamp { get; private set; }

    protected Event()
    {
        Timestamp = DateTime.Now.ToUniversalTime().AddHours(-3);
    }
}