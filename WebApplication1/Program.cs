using System.ComponentModel;
using WebApplication1;
using WebApplication1.Data;
using WebApplication1.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//Dependency Injection
builder.Services.AddSingleton<ICategory, CategoryADO>();

builder.Services.AddSingleton<IInstructor, InstructorADO>();

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

// app.MapGet("api/v1/categories", (ICategory categoryData)=>
// {
//     var categories = categoryData.GetCategories();
//     return categories;
// });

// app.MapGet("api/v1/categories/{id}", (ICategory categoryData, int id) =>
// {
//     var category = categoryData.GetCategoryById(id);
//     return category;
// });

// app.MapPost("api/v1/categories", (ICategory categoryData, Category category) =>
// {
//     var newCategory = categoryData.AddCategory(category);
//     return newCategory;
// });

// app.MapPut("api/v1/categories", (ICategory categoryData, Category category) =>
// {
//     var updatedCategory = categoryData.UpdateCategory(category);
//     return updatedCategory;
// });
// app.MapDelete("api/v1/categories/{id}", (ICategory categoryData, int id) =>
// {
//     categoryData.DeleteCategory(id);
//     return Results.NoContent();
// });

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

app.Run();

