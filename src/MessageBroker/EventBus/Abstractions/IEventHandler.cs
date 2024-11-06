namespace EventBus.Abstractions;
public interface IEventHandler<T> where T : IEvent
{
    Task HandleEventAsync(T eventData);
}
