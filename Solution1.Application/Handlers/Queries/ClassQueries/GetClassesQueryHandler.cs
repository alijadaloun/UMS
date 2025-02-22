using MediatR;
using Solution1.Domain.Entities;
using Solution1.Persistence.Repositories;

namespace Solution1.Application.Handlers.Queries.ClassQueries;
public record GetClassesQuery(): IRequest<List<Class>>;
public class GetClassesQueryHandler: IRequestHandler<GetClassesQuery, List<Class>>
{
    private readonly ClassRepository _classRepository;

    public GetClassesQueryHandler(ClassRepository classRepository)
    {
        _classRepository = classRepository;
    }

    public async Task<List<Class>> Handle(GetClassesQuery query, CancellationToken token)
    {
       var classes = await _classRepository.GetAll();
       if (classes == null) throw new ArgumentNullException(  "No classes found");
       return classes;

    }
}