namespace EnrollmentMS.Persistence;

public interface IEventBus
{
    void Subscribe<T, TH>()where TH : IIntegrationEventHandler<T>;
}