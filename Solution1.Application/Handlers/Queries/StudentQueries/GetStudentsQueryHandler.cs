using MediatR;
using Solution1.Domain.Entities;
using Solution1.Persistence.Repositories;

namespace Solution1.Application.Handlers.Queries.StudentQueries;

public record GetStudentsQuery(): IRequest<List<Student>>;

public class GetStudentsQueryHandler: IRequestHandler<GetStudentsQuery, List<Student>>
{
    private readonly StudentRepository _studentRepository;

    public GetStudentsQueryHandler(StudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }


    public async Task<List<Student>> Handle(GetStudentsQuery request, CancellationToken cancellationToken)
    {
        var students =   await _studentRepository.GetAll();
        return students;
    }
}