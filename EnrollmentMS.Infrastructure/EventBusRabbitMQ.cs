using RabbitMQ.Client;
using System;
using System.Text;
using EnrollmentMS.Infrastructure;
using EnrollmentMS.Persistence;

public class EventBusRabbitMQ : IEventBus, IDisposable
{
    private readonly IRabbitMQPersistentConnection _persistentConnection;
    private readonly EventBusSubscriptionManager _subsManager;
    private readonly string _queueName = "event_bus_queue";
    private const string BROKER_NAME = "microservice_event_bus";

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