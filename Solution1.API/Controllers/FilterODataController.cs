using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Solution1.Domain.Entities;
using Solution1.Persistence.Repositories;

namespace Solution1.Presentation.Controllers;
[Route("odata")]
public class FilterODataController: ODataController
{
    private readonly ClassRepository _classRepository;
    private readonly CourseRepository _courseRepository;
    private readonly StudentRepository _studentRepository;
    private readonly TeacherRepository _teacherRepository;

    public FilterODataController(ClassRepository classRepository, 
       TeacherRepository teacherRepository, 
        CourseRepository courseRepository, 
        StudentRepository studentRepository)
    {
        _classRepository = classRepository;
        _courseRepository = courseRepository;
        _studentRepository = studentRepository;
        _teacherRepository = teacherRepository;
        
    }
    [EnableQuery]
    [HttpGet("classes")]
    public async Task<ActionResult<IEnumerable<Class>>> GetClasses()
    {
        return Ok(await _classRepository.GetAll());
    }

    [EnableQuery]
    [HttpGet("class/{key}")]
    public async Task<ActionResult<Class>> GetClass([FromRoute] int key)
    {
        var c = await _classRepository.Get(key);
        if (c == null) return NotFound();
        return Ok(c);

    }
    [EnableQuery]
    [HttpGet("courses")]
    public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
    {
        return Ok(await _courseRepository.GetAll());
    }

    [EnableQuery]
    [HttpGet("course/{key}")]
    public async Task<ActionResult<Class>> GetCourse([FromRoute] int key)
    {
        var c = await _courseRepository.Get(key);
        if (c == null) return NotFound();
        return Ok(c);

    }
    [EnableQuery]
    [HttpGet("students")]
    public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
    {
        return Ok(await _studentRepository.GetAll());
    }

    [EnableQuery]
    [HttpGet("student/{key}")]
    public async Task<ActionResult<Class>> GetStudent([FromRoute] int key)
    {
        var c = await _studentRepository.GetById(key);
        if (c == null) return NotFound();
        return Ok(c);

    }
    [EnableQuery]
    [HttpGet("teachers")]
    public async Task<ActionResult<IEnumerable<Teacher>>> GetTeachers()
    {
        return Ok(await _teacherRepository.GetTeachers());
    }

    [EnableQuery]
    [HttpGet("teacher/{key}")]
    public async Task<ActionResult<Teacher>> GetTeacher([FromRoute] int key)
    {
        var c = await _teacherRepository.GetById(key);
        if (c == null) return NotFound();
        return Ok(c);

    }
}