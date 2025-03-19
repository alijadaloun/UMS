using EnrollmentMS.Persistence;

namespace EnrollmentMS.Infrastructure;

public class EventBusRabbitMQ : IEventBus, IDisposable
{
    private readonly IRabbitMQPersistentConnection _persistentConnection;
    private readonly EventBusSubscriptionManager _subsManager;

    public EventBusRabbitMQ(IRabbitMQPersistentConnection persistentConnection, EventBusSubscriptionManager subsManager)
    {
        _persistentConnection = persistentConnection ?? throw new ArgumentNullException(nameof(persistentConnection));
        _subsManager = subsManager ?? throw new ArgumentNullException(nameof(subsManager));
    }

    public void Subscribe<T, TH>() where TH : IIntegrationEventHandler<T>
    {
        var eventName = _subsManager.GetEventKey<T>();

        var containsKey = _subsManager.HasSubscriptionsForEvent(eventName);
        if (!containsKey)
        {
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }

            _persistentConnection.CreateModel();
            
            
            
        }

        _subsManager.AddSubscription<T, TH>();
    }


    public void Dispose()
    {
        
    }
}