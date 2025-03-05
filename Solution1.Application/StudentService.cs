using MassTransit;
using Shared;
using Solution1.Domain.Entities;
using Solution1.Persistence.Repositories;

namespace Solution1.Application;

public class StudentService
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly StudentRepository _studentRepository;

    public StudentService(IPublishEndpoint publishEndpoint, StudentRepository studentRepository)
    {
        _publishEndpoint = publishEndpoint;
        _studentRepository = studentRepository;
    }

    public async Task<int> CreateStudentAsync(Student student)
    {
        await _studentRepository.Add(student);

        await _publishEndpoint.Publish(new StudentCreatedEvent
        {
            StudentId = student.Id,
            Name = student.StudentName
        });

        return student.Id;
    }
}