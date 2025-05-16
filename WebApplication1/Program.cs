using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using WebApplication1;
using WebApplication1.Data;
using WebApplication1.DTO;
using WebApplication1.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//add ef core
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Dependency Injection
builder.Services.AddScoped<ICategory, CategoryEF>();
builder.Services.AddScoped<ICourse, CourseEF>();
builder.Services.AddScoped<IInstructor, InstructorEF>();
// builder.Services.AddSingleton<IInstructor, InstructorADO>();
// builder.Services.AddSingleton<ICourse, CourseADO>();

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.MapOpenApi();
// }

app.UseHttpsRedirection();


// app.MapGet("api/v1/helloservices", (string id)=> 
// {
//     return $"Hello ASP Web API : id ={id}!";
// });  

// app.MapGet("api/v1/helloservices/{name}", (string name)=> $"Hello {name}!");

// app.MapGet("api/v1/luas-segitiga/", (double alas, double tinggi)=> 
// {
//     double luas = 0.5 * alas * tinggi;
//     return $"Luas segitiga dengan alas = {alas} dan tinggi = {tinggi} adalah {luas}";
// });

app.MapGet("api/v1/categories", (ICategory categoryData)=>
{
    var categories = categoryData.GetCategories();
    return categories;
});

app.MapGet("api/v1/categories/{id}", (ICategory categoryData, int id) =>
{
    var category = categoryData.GetCategoryById(id);
    return category;
});

app.MapPost("api/v1/categories", (ICategory categoryData, Category category) =>
{
    var newCategory = categoryData.AddCategory(category);
    return newCategory;
});

app.MapPut("api/v1/categories", (ICategory categoryData, Category category) =>
{
    var updatedCategory = categoryData.UpdateCategory(category);
    return updatedCategory;
});
app.MapDelete("api/v1/categories/{id}", (ICategory categoryData, int id) =>
{
    categoryData.DeleteCategory(id);
    return Results.NoContent();
});

app.MapGet("api/v1/instructors", (IInstructor instructorData)=>
{
    var instructors = instructorData.GetInstructor();
    return instructors;
});

app.MapGet("api/v1/instructors/{id}", (IInstructor instructorData, int id) =>
{
    var instructor = instructorData.GetInstructorById(id);
    return instructor;
});

app.MapPost("api/v1/instructors", (IInstructor instructorData, Instructor instructor) =>
{
    var newInstructor = instructorData.AddInstructor(instructor);
    return newInstructor;
});

app.MapPut("api/v1/instructors", (IInstructor instructorData, Instructor instructor) =>
{
    var updatedInstructor = instructorData.UpdateInstructor(instructor);
    return updatedInstructor;
});
app.MapDelete("api/v1/instructors/{id}", (IInstructor instructorData, int id) =>
{
    instructorData.DeleteInstructor(id);
    return Results.NoContent();
});

app.MapGet("api/v1/courses", (ICourse courseData)=>
{
    var courses = courseData.GetAllCourses();
    var courseDTOs = courses.Select(course => new CourseDTO
    {
        CourseId = course.CourseId,
        CourseName = course.CourseName,
        CourseDescription = course.CourseDescription,
        Duration = course.Duration,
        Category = course.Category == null ? null : new CategoryDTO
        {
            CategoryId = course.Category.CategoryId,
            CategoryName = course.Category.CategoryName
        },
        Instructor = course.Instructor == null ? null : new InstructorDTO
        {
            InstructorId = course.Instructor.InstructorId,
            InstructorName = course.Instructor.InstructorName
        }
    });
    return Results.Ok(courseDTOs);
});

app.MapGet("api/v1/courses/{id}", (ICourse courseData, int id) =>
{
    var course = courseData.GetCourseByIdCourse(id);
    if (course == null) return Results.NotFound();

    var courseDTO = new CourseDTO
    {
        CourseId = course.CourseId,
        CourseName = course.CourseName,
        CourseDescription = course.CourseDescription,
        Duration = course.Duration,
        Category = course.Category == null ? null : new CategoryDTO
        {
            CategoryId = course.Category.CategoryId,
            CategoryName = course.Category.CategoryName
        },
        Instructor = course.Instructor == null ? null : new InstructorDTO
        {
            InstructorId = course.Instructor.InstructorId,
            InstructorName = course.Instructor.InstructorName
        }
    };
    return Results.Ok(courseDTO);
});

app.MapPost("api/v1/courses", (ICourse courseData, CourseAddDTO courseAddDTO) =>
{
    var course = new Course
    {
        CourseName = courseAddDTO.CourseName,
        CourseDescription = courseAddDTO.CourseDescription,
        Duration = courseAddDTO.Duration,
        CategoryId = courseAddDTO.CategoryId,
        InstructorId = courseAddDTO.InstructorId
    };

    var added = courseData.AddCourse(course);

    var courseDTO = new CourseDTO
    {
        CourseId = added.CourseId,
        CourseName = added.CourseName,
        CourseDescription = added.CourseDescription,
        Duration = added.Duration,
        Category =  new CategoryDTO
        {
            CategoryId = added.CategoryId,
            CategoryName = added.Category?.CategoryName??""
        },
        Instructor = new InstructorDTO
        {
            InstructorId = added.InstructorId,
            InstructorName = added.Instructor?.InstructorName??""
        },
    };
    return Results.Ok(courseDTO);  
});

app.MapPut("api/v1/courses", (ICourse courseData, Course course) =>
{
    var existingCourse = courseData.GetCourseByIdCourse(course.CourseId);
    if (existingCourse == null)
        return Results.NotFound();

    existingCourse.CourseName = course.CourseName;
    existingCourse.CourseDescription = course.CourseDescription;
    existingCourse.Duration = course.Duration;
    existingCourse.CategoryId = course.CategoryId;
    existingCourse.InstructorId = course.InstructorId;
    
    var updated = courseData.UpdateCourse(existingCourse);

    var courseDTO = new CourseDTO
    {
        CourseId = updated.CourseId,
        CourseName = updated.CourseName,
        CourseDescription = updated.CourseDescription,
        Duration = updated.Duration,
        Category = updated.Category == null ? null : new CategoryDTO
        {
            CategoryId = updated.Category.CategoryId,
            CategoryName = updated.Category.CategoryName
        },
        Instructor = updated.Instructor == null ? null : new InstructorDTO
        {
            InstructorId = updated.Instructor.InstructorId,
            InstructorName = updated.Instructor.InstructorName
        }
    };

    return Results.Ok(courseDTO);
});
app.MapDelete("api/v1/courses/{id}", (ICourse courseData, int id) =>
{
    try
    {
        courseData.DeleteCourse(id);
        return Results.NoContent();
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

app.Run();

