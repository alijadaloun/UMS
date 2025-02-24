using MediatR;
using Solution1.Domain.Entities;
using Solution1.Infrastructure.Cache;
using Solution1.Persistence.Repositories;

namespace Solution1.Application.Handlers.Queries.CourseQueries;

public record GetCoursesQuery(): IRequest<List<Course>>;
public class GetCoursesQueryHandler: IRequestHandler<GetCoursesQuery, List<Course>>
{
    private readonly CourseRepository _courseRepository;
    private readonly RedisCacheService _redisCacheService;
    
    public GetCoursesQueryHandler(CourseRepository courseRepository, RedisCacheService redisCacheService)
    {
        _courseRepository = courseRepository;
        _redisCacheService = redisCacheService;
    }

    public async Task<List<Course>> Handle(GetCoursesQuery request, CancellationToken cancellationToken)
    {
        string key = "courses:all";
        var cached = await _redisCacheService.GetAsync<List<Course>>( key);
        if (cached != null) return cached;
        var courses = await _courseRepository.GetAll();
        await _redisCacheService.SetAsync(key, courses, new TimeSpan(0, 30, 0));   
        return courses;
    }
    
}