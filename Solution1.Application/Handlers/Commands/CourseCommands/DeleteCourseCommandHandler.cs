using MediatR;
using Solution1.Persistence.Repositories;

namespace Solution1.Application.Handlers.Commands.CourseCommands;

public record DeleteCourseCommand(int id) : IRequest<int>;
public class DeleteCourseCommandHandler
    : IRequestHandler<DeleteCourseCommand, int>
{
    private readonly CourseRepository _courseRepository;

    public DeleteCourseCommandHandler(CourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<int> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
    {
        var c = await _courseRepository.Delete(request.id);
        await _courseRepository.SaveChanges();
        return c.Id;
    }
    
}