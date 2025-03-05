using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using EnrollmentMS.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

public class DefaultRabbitMQPersistentConnection : IRabbitMQPersistentConnection
{
    private readonly IConnectionFactory _connectionFactory;
    private IConnection _connection;
    private readonly object _lock = new();
    private bool _disposed;

    public DefaultRabbitMQPersistentConnection(IConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
    }

    public bool IsConnected => _connection != null && _connection.IsOpen && !_disposed;

    public bool TryConnect()
    {
        lock (_lock)
        {
            try
            {
                _connection = _connectionFactory.CreateConnection();
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