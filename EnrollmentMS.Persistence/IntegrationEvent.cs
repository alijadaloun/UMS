namespace EnrollmentMS.Persistence;

public abstract class IntegrationEvent
{
    public int Id { get; }
    public DateTime CreationDate { get; }

    public IntegrationEvent()
    {
        Id = 1;
        CreationDate = DateTime.UtcNow;
    }
}