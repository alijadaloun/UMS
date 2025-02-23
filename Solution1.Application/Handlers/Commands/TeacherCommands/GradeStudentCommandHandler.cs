using MediatR;
using Solution1.Persistence.Repositories;

namespace Solution1.Application.Handlers.Commands.TeacherCommands;
public record GradeStudentCommand(int StudentID, int grade): IRequest<int>;
public class GradeStudentCommandHandler: IRequestHandler<GradeStudentCommand,int>
{
    private readonly TeacherRepository _teacherRepository;

    public GradeStudentCommandHandler(TeacherRepository teacherRepository)
    {
        _teacherRepository = teacherRepository;
    }

    public async Task<int> Handle(GradeStudentCommand request, CancellationToken cancellationToken)
    {
        await _teacherRepository.Grade(request.StudentID, request.grade);
        return request.StudentID;
        
    }
    
}