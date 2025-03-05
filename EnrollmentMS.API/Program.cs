using EnrollmentMS.Infrastructure;
using EnrollmentMS.Persistence;
using MassTransit;
using RabbitMQ.Client;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<EnrollmentDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("EnrollmentDatabase")));

builder.Services.AddScoped< EnrollmentRepository>();

builder.Services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
{
    var factory = new ConnectionFactory()
    {
        HostName = "localhost"
    };

    return new DefaultRabbitMQPersistentConnection(factory);
});

builder.Services.AddSingleton<IEventBus, EventBusRabbitMQ>();

builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<EnrollStudentEventHandler>();

    config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host("rabbitmq://localhost");
        //bydefault masstransit take 5672

        cfg.ReceiveEndpoint("enrollment_queue", e =>
        {
            e.ConfigureConsumer<EnrollStudentEventHandler>(ctx); 
        });
    });
});

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();