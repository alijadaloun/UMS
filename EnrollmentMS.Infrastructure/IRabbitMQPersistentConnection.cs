using Microsoft.EntityFrameworkCore.Metadata;

namespace EnrollmentMS.Infrastructure;

public interface IRabbitMQPersistentConnection
{
    bool IsConnected { get; }
    bool TryConnect();
    IModel CreateModel();
}