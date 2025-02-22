using MediatR;
using Solution1.Domain.Entities;
using Solution1.Persistence.Repositories;

namespace Solution1.Application.Handlers.Queries.StudentQueries;

public record GetStudentByIdQuery(int id) : IRequest<Student>;

public class GetStudentByIdQueryHandler: IRequestHandler<GetStudentByIdQuery, Student>
{
    private readonly StudentRepository _studentRepository;

    public GetStudentByIdQueryHandler(StudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<Student> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
    {
        var student = await _studentRepository.GetById(request.id);
        return student;
    }
}