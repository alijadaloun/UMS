using MediatR;
using Microsoft.AspNetCore.Http;
using Solution1.Persistence.Repositories;

namespace Solution1.Application.Handlers.Commands.UserCommands;

public record UploadProfilePictureCommand(int id, string password, IFormFile file) : IRequest<bool>;
public class UploadProfilePictureCommandHandler: IRequestHandler<UploadProfilePictureCommand, bool>
{
    private readonly UserRepository _userRepository;

    public UploadProfilePictureCommandHandler(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> Handle(UploadProfilePictureCommand request, CancellationToken cancellationToken)
    {
        var b = await _userRepository.AddProfilePicture(request.id,request.password,request.file);
        return b;
        
    }

}