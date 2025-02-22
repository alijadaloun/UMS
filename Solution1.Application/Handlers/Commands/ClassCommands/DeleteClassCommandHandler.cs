using MediatR;
using Solution1.Domain.Entities;
using Solution1.Persistence.Repositories;

namespace Solution1.Application.Handlers.Commands.ClassCommands;

public record DeleteClassCommand(int id): IRequest<int>;
public class DeleteClassCommandHandler: IRequestHandler<DeleteClassCommand, int>
{
    private readonly ClassRepository _classRepository;

    public DeleteClassCommandHandler(ClassRepository classRepository)
    {
        _classRepository = classRepository;
    }

    public async Task<int> Handle(DeleteClassCommand command, CancellationToken cancellationToken)
    {
        await _classRepository.Delete(command.id);
        await _classRepository.SaveChanges();
        return command.id;
    }
    
}