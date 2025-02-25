using MediatR;
using Solution1.Infrastructure;
using Solution1.Persistence.Repositories;

namespace Solution1.Application.Handlers.Commands.HangFireCommands;

public record HangFireNotifyCommand(int studentId, int grade) : IRequest<string>;
public class HangFireNotifyCommandHandler: IRequestHandler<HangFireNotifyCommand,string>
{
    private readonly TeacherRepository _teacherRepository;
    private readonly HangfireService _service;

    public HangFireNotifyCommandHandler(TeacherRepository teacherRepository, StudentRepository studentRepository, HangfireService service)
    {
        _teacherRepository = teacherRepository;
        _service = service;
    }

    public async Task<string> Handle(HangFireNotifyCommand request, CancellationToken cancellationToken)
    {
        string email =  _teacherRepository.GradeBackground( request.studentId, request.grade);
       await  _teacherRepository.SaveChanges();
        _service.ScheduleHourly<TeacherRepository>(x=>x.GradeBackground(request.studentId, request.grade));
        return email;
    }
    
}