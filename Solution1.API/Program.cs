using System.Reflection;
using Solution1.Persistence.Database;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Solution1.Application.Handlers.Commands;
using Solution1.Application.Handlers.Commands.ClassCommands;
using Solution1.Application.Handlers.Commands.CourseCommands;
using Solution1.Application.Handlers.Commands.StudentCommands;
using Solution1.Application.Handlers.Commands.TeacherCommands;
using Solution1.Application.Handlers.Queries;
using Solution1.Application.Handlers.Queries.ClassQueries;
using Solution1.Application.Handlers.Queries.CourseQueries;
using Solution1.Application.Handlers.Queries.StudentQueries;
using Solution1.Application.Handlers.Queries.TeacherQueries;
using Solution1.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<UniversityDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Host=localhost;Port=5432;Database=unidb;Username=ALIJAD;Password=alijad")));

// builder.Services.AddMediatR();
// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetStudentByIdQuery).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetStudentsQuery).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AddStudentCommand).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DeleteStudentCommand).Assembly));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetTeacherByIdQuery).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetTeachersQuery).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AddTeacherCommand).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DeleteTeacherCommand).Assembly));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetClassByIdQuery).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetClassesQuery).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AddClassCommand).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DeleteClassCommand).Assembly));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetCourseByIdQuery).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetCoursesQuery).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AddCourseCommand).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DeleteCourseCommand).Assembly));

builder.Services.AddScoped<StudentRepository>();
builder.Services.AddScoped<TeacherRepository>();
builder.Services.AddScoped<ClassRepository>();
builder.Services.AddScoped<CourseRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
