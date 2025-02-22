using MediatR;
using Solution1.Domain.Entities;
using Solution1.Persistence.Repositories;

namespace Solution1.Application.Handlers.Queries.TeacherQueries;
public record GetTeacherByIdQuery(int Id): IRequest<Teacher>;
public class GetTeacherByIdQueryHandler: IRequestHandler<GetTeacherByIdQuery, Teacher>
{
    private readonly TeacherRepository _teacherRepository;

    public GetTeacherByIdQueryHandler(TeacherRepository teacherRepository)
    {
        _teacherRepository = teacherRepository;
    }

    public async Task<Teacher> Handle(GetTeacherByIdQuery request, CancellationToken cancellationToken)
    {
        var t = await _teacherRepository.GetById(request.Id);
        if (t == null) throw new ArgumentNullException(  "Teacher not found");
        return t;
    }
    
    
}