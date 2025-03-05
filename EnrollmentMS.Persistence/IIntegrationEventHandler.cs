namespace EnrollmentMS.Persistence;

public interface IIntegrationEventHandler<T>
{
    Task Handle(T @event);
}