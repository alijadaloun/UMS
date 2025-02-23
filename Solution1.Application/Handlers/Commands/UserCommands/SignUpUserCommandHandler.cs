using MediatR;
using Solution1.Domain.Entities;
using Solution1.Persistence.Repositories;

namespace Solution1.Application.Handlers.Commands.UserCommands;
public record SignUpUserCommand(string userName, string Email, string Password, int Role): IRequest<string>;
public class SignUpUserCommandHandler: IRequestHandler<SignUpUserCommand, string>
{
    private readonly UserRepository _userRepository;

    public SignUpUserCommandHandler( UserRepository userRepository)
    {
        _userRepository = userRepository;
        
    }

    public async Task<string> Handle(SignUpUserCommand request, CancellationToken cancellationToken)
    {
        await _userRepository.addUser(new User
        {
            Username = request.userName,
            Email = request.Email,
            Password = request.Password,
            Role = request.Role
        });
        switch (request.Role)
        {
            case 1:
                return "Student";
            case 2:
                return "Teacher";

            default:
                return "Admin";
            
        }
    }
    
    
}