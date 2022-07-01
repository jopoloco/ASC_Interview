using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;
using WebApplication1.Context;
using WebApplication1.Services;
using WebApplication1.ViewModels;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IProductsService, ProductsService>();
builder.Services.AddDbContext<DatabaseContext>(opt => opt.UseInMemoryDatabase("ASCDatabase"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
    AddTestData(dbContext);
}

app.Run();

static void AddTestData(DatabaseContext context)
{
    var rand = new Random();
    for (int i = 1; i <= 50; i++)
    {
        var month = rand.Next(1, 12);
        var day = rand.Next(1, 25);
        context.Courses.Add(new CourseVM
        {
            Id = i,
            Name = $"Course {i}",
            CreatedDate = new DateTime(DateTime.Now.Year, month, day),
        });
    }
    context.SaveChanges();

    for (int i = 1; i <= 100; i++)
    {
        var month = rand.Next(1, 12);
        var day = rand.Next(1, 25);
        var price = rand.NextDouble() * 10.0;
        var product = new ProductVM
        {
            Id = i,
            Name = $"Product {i}",
            Price = Math.Round((decimal)price, 2),
            CreatedDate = new DateTime(DateTime.Now.Year, month, day),
            Courses = new List<CourseVM>()
        };

        var numCourses = rand.Next(2, 5);
        for(int c = 0; c < numCourses; c++)
        {
            bool added = false;
            while (!added)
            {
                var courseId = rand.Next(1, 50);
                if(!product.Courses.Any(c => c.Id == courseId))
                {
                    var course = context.Courses.Include(c => c.Products).Single(c => c.Id == courseId);
                    product.Courses.Add(course);
                    course.Products.Add(product);
                    added = true;
                }
            }
        }

        context.Products.Add(product);
    }

    context.SaveChanges();
}