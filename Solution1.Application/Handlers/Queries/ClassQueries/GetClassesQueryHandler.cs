using MediatR;
using Solution1.Domain.Entities;
using Solution1.Infrastructure.Cache;
using Solution1.Persistence.Repositories;

namespace Solution1.Application.Handlers.Queries.ClassQueries;
public record GetClassesQuery(): IRequest<List<Class>>;
public class GetClassesQueryHandler: IRequestHandler<GetClassesQuery, List<Class>>
{
    private readonly ClassRepository _classRepository;
    
    
    private readonly RedisCacheService _redisCacheService;

    public GetClassesQueryHandler(ClassRepository classRepository, RedisCacheService redisCacheService)
    {
        _classRepository = classRepository;
        _redisCacheService = redisCacheService;
    }

    public async Task<List<Class>> Handle(GetClassesQuery query, CancellationToken token)
    {
        string cacheKey = $"classes:all";
        var cached = await _redisCacheService.GetAsync<List<Class>>(cacheKey);
        if (cached != null) return cached;
       var classes = await _classRepository.GetAll();
       await _redisCacheService.SetAsync(
           cacheKey, 
           classes,
           new TimeSpan(0, 30, 0)
       );
       return classes;

    }
}