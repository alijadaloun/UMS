using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Solution1.Persistence.Database;
using Microsoft.EntityFrameworkCore;

using Solution1.Application.Handlers.Commands.StudentCommands;
using Solution1.Persistence.Cache;
using Solution1.Persistence.Repositories;
using StackExchange.Redis;
using Microsoft.OData.ModelBuilder;
using Microsoft.AspNetCore.OData;
using Solution1.Domain.Entities;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<UniversityDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Host=localhost;Port=5432;Database=unidb;Username=ALIJAD;Password=alijad")));

builder.Services.AddControllers();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AddStudentCommand).Assembly));

builder.Services.AddScoped<StudentRepository>();
builder.Services.AddScoped<TeacherRepository>();
builder.Services.AddScoped<ClassRepository>();
builder.Services.AddScoped<CourseRepository>();
builder.Services.AddSingleton<IConnectionMultiplexer>(
    ConnectionMultiplexer.Connect("localhost:6379"));
builder.Services.AddScoped<RedisCacheService>();

builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
});
builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});



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
var modelBuilder = new ODataConventionModelBuilder();
modelBuilder.EntityType<Class>();
modelBuilder.EntityType<Course>();
modelBuilder.EntityType<Student>();
modelBuilder.EntityType<Teacher>();
modelBuilder.EntitySet<Class>("Classes");
modelBuilder.EntitySet<Course >("Courses");
modelBuilder.EntitySet<Student >("Students");
modelBuilder.EntitySet<Teacher >("Teachers");
builder.Services.AddControllers().AddOData(
    options => options.Select().Filter().OrderBy().Expand().Count().SetMaxTop(null)
        .AddRouteComponents("odata", modelBuilder.GetEdmModel())
);
var app = builder.Build();

app.UseHttpsRedirection();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
