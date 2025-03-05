using MassTransit;
using System.Threading.Tasks;
using Shared;

public class EventBusPublisher
{
    private readonly IPublishEndpoint _publishEndpoint;

    public EventBusPublisher(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public async Task PublishStudentCreated(int studentId, int courseId)
    {
        await _publishEndpoint.Publish(new StudentCreatedEvent
        {
            StudentId = studentId,
            CourseId = courseId
        });
    }
}