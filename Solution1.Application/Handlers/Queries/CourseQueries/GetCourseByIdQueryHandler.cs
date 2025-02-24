using MediatR;
using Solution1.Domain.Entities;
using Solution1.Infrastructure.Cache;
using Solution1.Persistence.Repositories;

namespace Solution1.Application.Handlers.Queries.CourseQueries;
public record GetCourseByIdQuery(int Id) : IRequest<Course>;
public class GetCourseByIdQueryHandler: IRequestHandler<GetCourseByIdQuery, Course>
{
    private readonly CourseRepository _courseRepository;
    private readonly RedisCacheService _redisCacheService;
    

    public GetCourseByIdQueryHandler(CourseRepository courseRepository, RedisCacheService redisCacheService)
    {
        _courseRepository = courseRepository;
        _redisCacheService = redisCacheService;
    }

    public async Task<Course> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
    {
        string key = $"course:{request.Id}";
        var cached = await _redisCacheService.GetAsync<Course>(key);
        if (cached != null) return cached;
        var c = await _courseRepository.Get(request.Id);
        if (c == null) throw new ArgumentNullException(  "Course not found");
        await _redisCacheService.SetAsync(key, c, new TimeSpan(0, 30, 0));
        return c;

    }
    
}