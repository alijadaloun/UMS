using MediatR;
using Solution1.Domain.Entities;
using Solution1.Persistence.Repositories;

namespace Solution1.Application.Handlers.Queries.TeacherQueries;
public record GetTeachersQuery(): IRequest<List<Teacher>>;
public class GetTeachersQueryHandler: IRequestHandler<GetTeachersQuery, List<Teacher>>
{
    private readonly TeacherRepository _teacherRepository;

    public GetTeachersQueryHandler(TeacherRepository teacherRepository)
    {
        _teacherRepository = teacherRepository;
    }

    public async Task<List<Teacher>> Handle(GetTeachersQuery request, CancellationToken cancellationToken)
    {
        return await _teacherRepository.GetTeachers();
    }
    
}