namespace EnrollmentMS.Persistence;

public class EventBusSubscriptionManager
{
    private readonly Dictionary<string, Type> _handlers = new();
    private readonly Dictionary<string, Type> _eventTypes = new();

    public void AddSubscription<T, TH>() where TH : IIntegrationEventHandler<T>
    {
        var eventName = typeof(T).Name;
        if (!_handlers.ContainsKey(eventName))
        {
            _handlers.Add(eventName, typeof(TH));
            _eventTypes.Add(eventName, typeof(T));
        }
    }

    public bool HasSubscriptionsForEvent(string eventName) => _handlers.ContainsKey(eventName);

    public string GetEventKey<T>() => typeof(T).Name;
}