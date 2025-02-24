using MediatR;
using Solution1.Domain.Entities;
using Solution1.Infrastructure.Cache;
using Solution1.Persistence.Repositories;

namespace Solution1.Application.Handlers.Queries.StudentQueries;

public record GetStudentByIdQuery(int id) : IRequest<Student>;

public class GetStudentByIdQueryHandler: IRequestHandler<GetStudentByIdQuery, Student>
{
    private readonly StudentRepository _studentRepository;
    private readonly RedisCacheService _redisCacheService;

    public GetStudentByIdQueryHandler(StudentRepository studentRepository, RedisCacheService redisCacheService)
    {
        _studentRepository = studentRepository;
        _redisCacheService = redisCacheService;
    }

    public async Task<Student> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
    {
        string key = $"student:{request.id}";
        var cached = await _redisCacheService.GetAsync<Student>(key);
        if (cached != null) return cached;
        var student = await _studentRepository.GetById(request.id);
        await _redisCacheService.SetAsync(key, student, new TimeSpan(0, 30, 0));
        return student;
    }
}