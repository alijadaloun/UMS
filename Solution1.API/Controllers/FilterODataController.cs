using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Solution1.Application.Handlers.Queries.ClassQueries;
using Solution1.Application.Handlers.Queries.StudentQueries;
using Solution1.Application.Handlers.Queries.TeacherQueries;
using Solution1.Domain.Entities;
using Solution1.Persistence.Repositories;

namespace Solution1.Presentation.Controllers;
[Route("odata")]
public class FilterODataController: ODataController
{
    private readonly IMediator _mediator;

    public FilterODataController(
       IMediator mediator)
    {
        _mediator = mediator;
        }
    [EnableQuery]
    [HttpGet("classes")]
    public async Task<ActionResult<IEnumerable<Class>>> GetClasses()
    {
        var result = await _mediator.Send(new GetClassesQuery());
        return Ok(result);
    }

    [EnableQuery]
    [HttpGet("class/{key}")]
    public async Task<ActionResult<Class>> GetClass([FromRoute] int key)
    {
        var c = await _mediator.Send(new GetClassByIdQuery(key));
        if (c == null) return NotFound();
        return Ok(c);

    }
    [EnableQuery]
    [HttpGet("courses")]
    public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
    {
        var result = await _mediator.Send(new GetClassesQuery());
        return Ok(result);
    }

    [EnableQuery]
    [HttpGet("course/{key}")]
    public async Task<ActionResult<Course>> GetCourse([FromRoute] int key)
    {
        var c = await _mediator.Send(new GetClassByIdQuery(key));
        if (c == null) return NotFound();
        return Ok(c);

    }
    [EnableQuery]
    [HttpGet("students")]
    public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
    {
        var result = await _mediator.Send(new GetStudentsQuery());
        return Ok(result);
    }

    [EnableQuery]
    [HttpGet("student/{key}")]
    public async Task<ActionResult<Student>> GetStudent([FromRoute] int key)
    {
        var c = await _mediator.Send(new GetStudentByIdQuery(key));
        if (c == null) return NotFound();
        return Ok(c);

    }
    [EnableQuery]
    [HttpGet("teachers")]
    public async Task<ActionResult<IEnumerable<Teacher>>> GetTeachers()
    {
        var result = await _mediator.Send(new GetTeachersQuery());
        return Ok(result);
    }

    [EnableQuery]
    [HttpGet("teacher/{key}")]
    public async Task<ActionResult<Teacher>> GetTeacher([FromRoute] int key)
    {
        var t = await _mediator.Send(new GetTeacherByIdQuery(key));
        if (t == null) return NotFound();
        return Ok(t);

    }
}