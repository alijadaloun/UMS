using MediatR;
using Solution1.Domain.Entities;
using Solution1.Persistence.Repositories;

namespace Solution1.Application.Handlers.Queries.ClassQueries;
public record GetClassByIdQuery(int ClassId) : IRequest<Class>;
public class GetClassByIdQueryHandler: IRequestHandler<GetClassByIdQuery, Class>
{
    private readonly ClassRepository _classRepository;

    public GetClassByIdQueryHandler(ClassRepository classRepository)
    {
        _classRepository = classRepository;
    }

    public async Task<Class> Handle(GetClassByIdQuery request, CancellationToken cancellationToken)
    {
        var c = await _classRepository.Get(request.ClassId);
        if (c == null) throw new ArgumentNullException(  "Class not found");
        return c;
        
        
    }
    
}