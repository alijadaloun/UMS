using RabbitMQ.Client;

namespace EnrollmentMS.Infrastructure;

public class DefaultRabbitMQPersistentConnection : IRabbitMQPersistentConnection
{
    private readonly IConnectionFactory _connectionFactory;
    private IConnection _connection;
    private readonly object _lock = new();
    private bool _disposed;

    public DefaultRabbitMQPersistentConnection(IConnectionFactory connectionFactory, IConnection connection)
    {
        _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        _connection = connection ?? throw new ArgumentNullException(nameof(connection));
    }

    public DefaultRabbitMQPersistentConnection(IConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
    }

    public bool IsConnected => _connection != null && _connection.IsOpen && !_disposed;

    public async  Task<bool> TryConnect()
    {

        try
        {
            _connection = await _connectionFactory.CreateConnectionAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"RabbitMQ connection failed: {ex.Message}");
            return false;
        }

        if (IsConnected)
        {
            Console.WriteLine($"Connected to RabbitMQ on {_connection.Endpoint.HostName}");
            return true;
        }
        else
        {
            return false;
        }
        
    }

    public void CreateModel()
    {
        if (!IsConnected)
        {
            throw new InvalidOperationException("RabbitMQ connection is not available.");
        }

    }

    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;

        try
        {
            _connection?.Dispose();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error while disposing RabbitMQ connection: {ex.Message}");
        }
    }
}