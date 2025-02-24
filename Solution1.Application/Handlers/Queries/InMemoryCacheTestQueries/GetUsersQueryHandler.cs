using MediatR;
using Solution1.Domain.Entities;
using Solution1.Infrastructure.Cache;
using Solution1.Persistence.Repositories;

namespace Solution1.Application.Handlers.Queries.InMemoryCacheTestQueries;

public record GetUsersQuery() : IRequest<List<User>>;
public class GetUsersQueryHandler: IRequestHandler<GetUsersQuery, List<User>>
{
    private readonly UserRepository _userRepository;
    private readonly InMemoryCacheService _inMemoryCacheService;

    public GetUsersQueryHandler(UserRepository userRepository, InMemoryCacheService inMemoryCacheService)
    {
        _userRepository = userRepository;
        _inMemoryCacheService = inMemoryCacheService;
    }

    public async Task<List<User>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        string cacheKey = "users:all";
        var users = await _inMemoryCacheService.GetOrAdd(cacheKey, () => _userRepository.GetUsers(), new TimeSpan(0, 5, 0));
        return users;




    }
    
}