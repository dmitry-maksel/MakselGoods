using EventBus.Abstractions;

namespace Reviews.API.IntegrationEvents.Events;

public class DisplayNameChangedEvent : IEvent
{
    public int UserId { get; set; }

    public string DisplayName { get; set; } = null!;
}
