using MediatR;
using Solution1.Domain.Entities;
using Solution1.Persistence.Repositories;

namespace Solution1.Application.Handlers.Commands.StudentCommands;

public record AddStudentCommand(string name, string email, string password) : IRequest<int>;
public class AddStudentCommandHandler: IRequestHandler<AddStudentCommand, int>
{
    private readonly StudentRepository _studentRepository;

    public AddStudentCommandHandler(StudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }


    public async Task<int> Handle(AddStudentCommand request, CancellationToken cancellationToken)
    { 
        var t= await _studentRepository.Add( new Student
        {
            Id=  _studentRepository.GetAll().Result.Last().Id+1,
            StudentName = request.name,
            Email = request.email,
            Password = request.password
        });
        await  _studentRepository.SaveChanges();
        return t;
    }
    
    
    
}