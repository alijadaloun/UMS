using System.Globalization;
using System.Reflection;
using Hangfire;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Solution1.Persistence.Database;
using Microsoft.EntityFrameworkCore;

using Solution1.Application.Handlers.Commands.StudentCommands;
using Solution1.Persistence.Repositories;
using StackExchange.Redis;
using Microsoft.OData.ModelBuilder;
using Microsoft.AspNetCore.OData;
using Microsoft.Extensions.Localization;
using Solution1.Domain.Entities;
using Solution1.Infrastructure.Cache;
using Serilog;
using Solution1.Infrastructure;
using Solution1.Presentation.Controllers;

// Log.Logger = new LoggerConfiguration().WriteTo.File($"logs/log{RollingInterval.Day}.txt").CreateLogger();

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<UniversityDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Nqgsql")));
builder.Host.UseSerilog();

builder.Services.AddControllers();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AddStudentCommand).Assembly));
builder.Services.AddScoped<StudentRepository>();
builder.Services.AddScoped<TeacherRepository>();
builder.Services.AddScoped<ClassRepository>();
builder.Services.AddScoped<CourseRepository>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddSingleton<IConnectionMultiplexer>(
    ConnectionMultiplexer.Connect("localhost:6379,abortConnect=false"));
builder.Services.AddScoped<HangfireService>();
builder.Services.AddScoped<RedisCacheService>();
builder.Services.AddMemoryCache();
builder.Services.AddScoped<InMemoryCacheService>();


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
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

var supportedCultures = new[]
{
    new CultureInfo("en"), new CultureInfo("de")   
};
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("de");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
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

app.UseRequestLocalization();
app.MapGet("/", (IStringLocalizer<UniversalController> localizer) =>
    localizer["HelloWorld"]); 

app.UseCors("AllowAll");
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
