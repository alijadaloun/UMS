using MediatR;
using Solution1.Domain.Entities;
using Solution1.Infrastructure.Cache;
using Solution1.Persistence.Repositories;

namespace Solution1.Application.Handlers.Queries.TeacherQueries;
public record GetTeacherByIdQuery(int Id): IRequest<Teacher>;
public class GetTeacherByIdQueryHandler: IRequestHandler<GetTeacherByIdQuery, Teacher>
{
    private readonly TeacherRepository _teacherRepository;
    private readonly RedisCacheService _redisCacheService;

    public GetTeacherByIdQueryHandler(TeacherRepository teacherRepository, RedisCacheService redisCacheService)
    {
        _teacherRepository = teacherRepository;
        _redisCacheService = redisCacheService;
    }

    public async Task<Teacher> Handle(GetTeacherByIdQuery request, CancellationToken cancellationToken)
    {
        string key = $"teacher:{request.Id}";
        var cached = await _redisCacheService.GetAsync<Teacher>(key);
        if (cached != null) return cached;
        var t = await _teacherRepository.GetById(request.Id);
        await _redisCacheService.SetAsync(key, t, new TimeSpan(0, 30, 0));
        return t;
    }
    
    
}