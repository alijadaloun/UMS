using MediatR;
using Solution1.Domain.Entities;
using Solution1.Infrastructure.Cache;
using Solution1.Persistence.Repositories;

namespace Solution1.Application.Handlers.Queries.ClassQueries;
public record GetClassByIdQuery(int ClassId) : IRequest<Class>;
public class GetClassByIdQueryHandler: IRequestHandler<GetClassByIdQuery, Class>
{
    private readonly ClassRepository _classRepository;
    private readonly RedisCacheService _redisCacheService;

    public GetClassByIdQueryHandler(ClassRepository classRepository, RedisCacheService redisCacheService)
    {
        _classRepository = classRepository;
        _redisCacheService = redisCacheService;
        
    }

    public async Task<Class> Handle(GetClassByIdQuery request, CancellationToken cancellationToken)
    {
        string cacheKey = $"class:{request.ClassId}";
        var cached = await _redisCacheService.GetAsync<Class>(cacheKey);
        if (cached != null) return cached;
        var c = await _classRepository.Get(request.ClassId);
        if (c == null) throw new ArgumentNullException(  "Class not found");
        await _redisCacheService.SetAsync(
            cacheKey, 
            c,
            new TimeSpan(0, 30, 0)
        );
        return c;
        
        
    }
    
}