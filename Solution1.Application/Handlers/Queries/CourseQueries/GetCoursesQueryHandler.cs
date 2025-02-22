using MediatR;
using Solution1.Domain.Entities;
using Solution1.Persistence.Repositories;

namespace Solution1.Application.Handlers.Queries.CourseQueries;

public record GetCoursesQuery(): IRequest<List<Course>>;
public class GetCoursesQueryHandler: IRequestHandler<GetCoursesQuery, List<Course>>
{
    private readonly CourseRepository _courseRepository;

    public GetCoursesQueryHandler(CourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<List<Course>> Handle(GetCoursesQuery request, CancellationToken cancellationToken)
    {
        var courses = await _courseRepository.GetAll();
        return courses;
    }
    
}