using MediatR;
using Solution1.Domain.Entities;
using Solution1.Persistence.Repositories;

namespace Solution1.Application.Handlers.Commands.CourseCommands;

public record AddCourseCommand(string name, string department) : IRequest<int>;
public class AddCourseCommandHandler
    : IRequestHandler<AddCourseCommand, int>
{
    private readonly CourseRepository _courseRepository;

    public AddCourseCommandHandler(CourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<int> Handle(AddCourseCommand request, CancellationToken cancellationToken)
    {
        var c = await _courseRepository.Add(new Course
        {
 
            CourseName = request.name,
            Department = request.department


        });
        await _courseRepository.SaveChanges();
        return c.Id;
    }
    
}