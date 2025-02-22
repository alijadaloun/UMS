using System.Windows.Input;
using MediatR;
using Solution1.Domain.Entities;
using Solution1.Persistence.Repositories;

namespace Solution1.Application.Handlers.Commands.StudentCommands;

public record RegisterCourseCommand(int StudentId, int CourseId, int TeacherId) : IRequest<Student>;

public class RegisterCourseCommandHandler : IRequestHandler<RegisterCourseCommand, Student>
{
    private StudentRepository _studentRepository;

    public RegisterCourseCommandHandler(StudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<Student> Handle(RegisterCourseCommand request, CancellationToken cancellationToken)
    {
       var student= await _studentRepository.RegisterStudent(request.StudentId, request.CourseId, request.TeacherId);
       await _studentRepository.SaveChanges();
       return student;
    }

}
