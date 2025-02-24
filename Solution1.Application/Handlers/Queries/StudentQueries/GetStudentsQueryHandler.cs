using MediatR;
using Solution1.Domain.Entities;
using Solution1.Infrastructure.Cache;
using Solution1.Persistence.Repositories;

namespace Solution1.Application.Handlers.Queries.StudentQueries;

public record GetStudentsQuery(): IRequest<List<Student>>;

public class GetStudentsQueryHandler: IRequestHandler<GetStudentsQuery, List<Student>>
{
    private readonly StudentRepository _studentRepository;
    private readonly RedisCacheService _redisCacheService;

    public GetStudentsQueryHandler(StudentRepository studentRepository, RedisCacheService redisCacheService)
    {
        _studentRepository = studentRepository;
        _redisCacheService = redisCacheService;
    }


    public async Task<List<Student>> Handle(GetStudentsQuery request, CancellationToken cancellationToken)
    {
        string key = "students:all";
        var cached = await _redisCacheService.GetAsync<List<Student>>(key);
        if (cached != null) return cached;
        var students =   await _studentRepository.GetAll();
        await _redisCacheService.SetAsync(key, students, new TimeSpan(0, 30, 0));
        return students;
    }
}