using MediatR;
using Solution1.Persistence.Repositories;

namespace Solution1.Application.Handlers.Commands.TeacherCommands;

public record DeleteTeacherCommand(int id) : IRequest<int>;
public class DeleteTeacherCommandHandler: IRequestHandler<DeleteTeacherCommand, int>
{
    private readonly TeacherRepository _teacherRepository;

    public DeleteTeacherCommandHandler(TeacherRepository teacherRepository)
    {
        _teacherRepository = teacherRepository;
    }

    public async Task<int> Handle(DeleteTeacherCommand request, CancellationToken cancellationToken)
    {
        await  _teacherRepository.Delete(request.id);
        await _teacherRepository.SaveChanges();
        return request.id;
        
    }
    
}