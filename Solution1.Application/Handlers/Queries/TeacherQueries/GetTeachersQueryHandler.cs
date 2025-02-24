using MediatR;
using Solution1.Domain.Entities;
using Solution1.Infrastructure.Cache;
using Solution1.Persistence.Repositories;

namespace Solution1.Application.Handlers.Queries.TeacherQueries;
public record GetTeachersQuery(): IRequest<List<Teacher>>;
public class GetTeachersQueryHandler: IRequestHandler<GetTeachersQuery, List<Teacher>>
{
    private readonly TeacherRepository _teacherRepository;
    private readonly RedisCacheService _redisCacheService;

    public GetTeachersQueryHandler(TeacherRepository teacherRepository, RedisCacheService redisCacheService)
    {
        _teacherRepository = teacherRepository;
        _redisCacheService = redisCacheService;
    }

    public async Task<List<Teacher>> Handle(GetTeachersQuery request, CancellationToken cancellationToken)
    {
        string key = "teachers:all";
        var cached = await _redisCacheService.GetAsync<List<Teacher>>(key);
        if (cached != null) return cached;
        var teachers = await _teacherRepository.GetTeachers();
        await _redisCacheService.SetAsync(key, teachers, new TimeSpan(0, 30, 0));
        return teachers;
    }
    
}