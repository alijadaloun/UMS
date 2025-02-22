using MediatR;
using Solution1.Domain.Entities;
using Solution1.Persistence.Repositories;

namespace Solution1.Application.Handlers.Queries.CourseQueries;
public record GetCourseByIdQuery(int Id) : IRequest<Course>;
public class GetCourseByIdQueryHandler: IRequestHandler<GetCourseByIdQuery, Course>
{
    private readonly CourseRepository _courseRepository;

    public GetCourseByIdQueryHandler(CourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<Course> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
    {
        var c = await _courseRepository.Get(request.Id);
        if (c == null) throw new ArgumentNullException(  "Course not found");
        await _courseRepository.SaveChanges();
        return c;

    }
    
}