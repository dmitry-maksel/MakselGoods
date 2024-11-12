using EventBus.Abstractions;

namespace Identity.API.Core.Events;

public class DisplayNameChangedEvent : IEvent
{
    public int UserId { get; set; }

    public string DisplayName { get; set; } = null!;
}
