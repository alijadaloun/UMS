using MediatR;
using Solution1.Infrastructure;
using Solution1.Persistence.Repositories;

namespace Solution1.Application.Handlers.Commands.HangFireCommands;
public record NotifyDailyDeadlineCommand(int studentId, string deadline): IRequest<string>;
public class NotifyDailyDeadlineCommandHandler: IRequestHandler<NotifyDailyDeadlineCommand, string >
{
    private readonly TeacherRepository _teacherRepository;
    
    private readonly HangfireService _service;

    public NotifyDailyDeadlineCommandHandler(TeacherRepository teacherRepository, HangfireService service)
    {
        _teacherRepository = teacherRepository;
        _service = service;
        
    }

    public async Task<string> Handle(NotifyDailyDeadlineCommand request, CancellationToken cancellationToken)
    {
        string email = _teacherRepository.Notify( request.studentId, request.deadline); 
        _service.ScheduleDaily<TeacherRepository>(x=>x.Notify(request.studentId, request.deadline));
        return email;
    }
    
}