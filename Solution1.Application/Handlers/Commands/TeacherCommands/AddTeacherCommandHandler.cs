using MediatR;
using Solution1.Domain.Entities;
using Solution1.Persistence.Repositories;

namespace Solution1.Application.Handlers.Commands.TeacherCommands;

public record AddTeacherCommand(string name, string email, string password) : IRequest<int>;

public class AddTeacherCommandHandler : IRequestHandler<AddTeacherCommand, int>
{
    private readonly TeacherRepository _teacherRepository;

    public AddTeacherCommandHandler(TeacherRepository teacherRepository)
    {
        _teacherRepository = teacherRepository;
    }
    public async Task<int> Handle(AddTeacherCommand request, CancellationToken cancellationToken){
        
        var t = await  _teacherRepository.Add(new Teacher
        {
            TeacherName = request.name,
            Email = request.email,
            Password = request.password
            
        });
        await _teacherRepository.SaveChanges();
        return t.Id;

    }
}