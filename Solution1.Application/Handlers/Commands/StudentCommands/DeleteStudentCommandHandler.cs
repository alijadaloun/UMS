using MediatR;
using Solution1.Persistence.Repositories;

namespace Solution1.Application.Handlers.Commands.StudentCommands;

public record DeleteStudentCommand(int id) : IRequest<int>;
public class DeleteStudentCommandHandler: IRequestHandler<DeleteStudentCommand, int>
{
    private readonly StudentRepository _studentRepository;

    public DeleteStudentCommandHandler(StudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<int> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
    {
        await _studentRepository.Delete(request.id);
        await _studentRepository.SaveChanges();
        return request.id;

    }
    
}