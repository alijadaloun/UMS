using MediatR;
using Solution1.Domain.Entities;
using Solution1.Persistence.Repositories;

namespace Solution1.Application.Handlers.Commands.ClassCommands;

public record AddClassCommand(string semester, int year, string room ) : IRequest<int>;
public class AddClassCommandHandler: IRequestHandler<AddClassCommand, int>
{
    private readonly ClassRepository _classRepository;

    public AddClassCommandHandler(ClassRepository classRepository)
    {
        _classRepository = classRepository;
    }

    public async Task<int> Handle(AddClassCommand request, CancellationToken cancellationToken)
    {
        var c = await _classRepository.Add(new Class
        {
            Semester = request.semester,
            Year = request.year,
            Room = request.room
        });
        await _classRepository.SaveChanges();
        return c;
    }
    
}