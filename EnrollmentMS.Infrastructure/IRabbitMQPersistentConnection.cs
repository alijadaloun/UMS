using Microsoft.EntityFrameworkCore.Metadata;

namespace EnrollmentMS.Infrastructure;

public interface IRabbitMQPersistentConnection
{
    bool IsConnected { get; }
    Task<bool> TryConnect();
    void CreateModel();
}