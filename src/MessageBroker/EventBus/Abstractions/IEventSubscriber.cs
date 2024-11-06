namespace EventBus.Abstractions;

public interface IEventSubscriber
{
    void Subscribe<T>(Func<T, Task> onMessage) where T : class;
}
