using MediatR;
using Solution1.Domain.Entities;
using Solution1.Infrastructure.Cache;
using Solution1.Persistence.Repositories;

namespace Solution1.Application.Handlers.Queries.InMemoryCacheTestQueries;
public record GetUserByIdQuery(int id): IRequest<User>;
public class GetUserByIdQueryHandler: IRequestHandler<GetUserByIdQuery, User>
{
    private readonly UserRepository _userRepository;
    private readonly InMemoryCacheService _inMemoryCacheService;

    public GetUserByIdQueryHandler( UserRepository userRepository,InMemoryCacheService inMemoryCacheService)
    {
        _userRepository = userRepository;
        _inMemoryCacheService = inMemoryCacheService;
    }

    public async Task<User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        string cacheKey = $"user:{request.id}";
        var user =await _inMemoryCacheService.GetOrAdd(cacheKey,() => _userRepository.GetUserById(request.id),new TimeSpan(0, 5, 0));
        return user;

    }
    
}