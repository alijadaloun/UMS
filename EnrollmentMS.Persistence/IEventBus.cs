namespace EnrollmentMS.Persistence;

public interface IEventBus
{
    void Subscribe<T, TH>();
}