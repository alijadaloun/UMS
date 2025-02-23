using MediatR;
using Solution1.Persistence.Repositories;

namespace Solution1.Application.Handlers.Commands.UserCommands;
public record SignInUserCommand(int id, string Password): IRequest<string>;
public class SignInUserCommandHandler: IRequestHandler<SignInUserCommand, string>
{
    private readonly UserRepository _userRepository;

    public SignInUserCommandHandler(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<string> Handle(SignInUserCommand request, CancellationToken cancellationToken)
    {
        var user =  await _userRepository.getUser(request.id, request.Password);
        return user;
    }
    
    
}