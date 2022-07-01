using Microsoft.EntityFrameworkCore;
using WebApplication1.Context;
using WebApplication1.ViewModels;

namespace WebApplication1.Services;

public class ProductsService : IProductsService
{
    public DatabaseContext DatabaseContext { get; set; }
    public ProductsService(DatabaseContext context)
    {
        DatabaseContext = context;
    }

    public async Task<ProductsVM> GetProducts(SearchQueryParams searchQuery, CancellationToken cancellationToken)
    {
        IQueryable<ProductVM> query = DatabaseContext.Products
            .Include(p => p.Courses);


        if (!string.IsNullOrWhiteSpace(searchQuery.SearchText))
        {
            query = query.Where(p => p.Name.Contains(searchQuery.SearchText) ||
                searchQuery.SearchText == p.Id.ToString());
        }

        if (!string.IsNullOrWhiteSpace(searchQuery.SortField) && searchQuery.Ascending.HasValue)
        {
            var ascending = searchQuery.Ascending.Value;
            switch (searchQuery.SortField)
            {
                case "ID":
                    if (ascending)
                    {
                        query = query.OrderBy(p => p.Id);
                    }
                    else
                    {
                        query = query.OrderByDescending(p => p.Id);
                    }
                    break;
                case "Name":
                    if (ascending)
                    {
                        query = query.OrderBy(p => p.Name);
                    }
                    else
                    {
                        query = query.OrderByDescending(c => c.Name);
                    }
                    break;
                case "CreatedDate":
                    if (ascending)
                    {
                        query = query.OrderBy(c => c.CreatedDate);
                    }
                    else
                    {
                        query = query.OrderByDescending(c => c.CreatedDate);
                    }
                    break;
                case "Price":
                    if (ascending)
                    {
                        query = query.OrderBy(c => c.Price);
                    }
                    else
                    {
                        query = query.OrderByDescending(c => c.Price);
                    }
                    break;
            }
        }

        var products = new ProductsVM
        {
            TotalProducts = query.Count(),
        };

        if (searchQuery.ItemsPerPage.HasValue && searchQuery.ItemsPerPage.Value > 0 && searchQuery.Page.HasValue)
        {
            query = query.Skip(searchQuery.Page.Value * searchQuery.ItemsPerPage.Value).Take(searchQuery.ItemsPerPage.Value);
        }

        products.PagedProducts = await query.ToListAsync(cancellationToken);

        return products;
    }

    public Task<ProductVM> GetProduct(int id, CancellationToken cancellationToken)
    {
        return DatabaseContext.Products
            .Include(p => p.Courses)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<ProductVM> UpdateProduct(int id, ProductVM updated, CancellationToken cancellationToken)
    {
        var product = await DatabaseContext.Products.Include(p => p.Courses).SingleAsync(p => p.Id == id, cancellationToken);
        product.Name = updated.Name;
        product.Price = updated.Price;

        DatabaseContext.SaveChanges();

        return product;
    }

    public async Task<ProductVM> AddCourseToProduct(int id, int courseId, CancellationToken cancellationToken)
    {
        var product = await DatabaseContext.Products.Include(p => p.Courses).SingleAsync(p => p.Id == id, cancellationToken);
        var course = await DatabaseContext.Courses.Include(c => c.Products).SingleAsync(c => c.Id == courseId, cancellationToken);

        product.Courses.Add(course);
        course.Products.Add(product);

        DatabaseContext.SaveChanges();

        return product;
    }

    public async Task<ProductVM> RemoveCourseFromProduct(int id, int courseId, CancellationToken cancellationToken)
    {
        var product = await DatabaseContext.Products.Include(p => p.Courses).SingleAsync(p => p.Id == id, cancellationToken);
        var course = await DatabaseContext.Courses.Include(c => c.Products).SingleAsync(c => c.Id == courseId, cancellationToken);

        product.Courses.Remove(product.Courses.Where(c => c.Id == courseId).First());
        course.Products.Remove(course.Products.Where(p => p.Id == id).First());

        DatabaseContext.SaveChanges();

        return product;
    }

    public async Task<CoursesVM> GetCourses(SearchQueryParams searchQuery, CancellationToken cancellationToken)
    {
        IQueryable<CourseVM> query = DatabaseContext.Courses;

        if (!string.IsNullOrWhiteSpace(searchQuery.SearchText))
        {
            query = query.Where(p => p.Name.Contains(searchQuery.SearchText) ||
                searchQuery.SearchText == p.Id.ToString());
        }

        if (!string.IsNullOrWhiteSpace(searchQuery.SortField) && searchQuery.Ascending.HasValue)
        {
            var ascending = searchQuery.Ascending.Value;
            switch (searchQuery.SortField)
            {
                case "ID":
                    if (ascending)
                    {
                        query = query.OrderBy(p => p.Id);
                    }
                    else
                    {
                        query = query.OrderByDescending(p => p.Id);
                    }
                    break;
                case "Name":
                    if (ascending)
                    {
                        query = query.OrderBy(p => p.Name);
                    }
                    else
                    {
                        query = query.OrderByDescending(c => c.Name);
                    }
                    break;
                case "CreatedDate":
                    if (ascending)
                    {
                        query = query.OrderBy(c => c.CreatedDate);
                    }
                    else
                    {
                        query = query.OrderByDescending(c => c.CreatedDate);
                    }
                    break;
            }
        }

        var courses = new CoursesVM
        {
            TotalCourses = query.Count(),
        };

        if (searchQuery.ItemsPerPage.HasValue && searchQuery.ItemsPerPage.Value > 0 && searchQuery.Page.HasValue)
        {
            query = query.Skip(searchQuery.Page.Value * searchQuery.ItemsPerPage.Value).Take(searchQuery.ItemsPerPage.Value);
        }

        courses.PagedCourses = await query.ToListAsync(cancellationToken);

        return courses;
    }

    public Task<CourseVM> GetCourse(int id, CancellationToken cancellationToken)
    {
        return DatabaseContext.Courses
            .Include(c => c.Products)
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task<CourseVM> UpdateCourse(int id, CourseVM updated, CancellationToken cancellationToken)
    {
        var course = await DatabaseContext.Courses.Include(c => c.Products).SingleAsync(c => c.Id == id, cancellationToken);
        course.Name = updated.Name;

        DatabaseContext.SaveChanges();

        return course;
    }

    public async Task<CourseVM> AddProductToCourse(int id, int productId, CancellationToken cancellationToken)
    {
        var product = await DatabaseContext.Products.Include(p => p.Courses).SingleAsync(p => p.Id == productId, cancellationToken);
        var course = await DatabaseContext.Courses.Include(c => c.Products).SingleAsync(c => c.Id == id, cancellationToken);

        product.Courses.Add(course);
        course.Products.Add(product);

        DatabaseContext.SaveChanges();

        return course;
    }

    public async Task<CourseVM> RemoveProductFromCourse(int id, int productId, CancellationToken cancellationToken)
    {
        var product = await DatabaseContext.Products.Include(p => p.Courses).SingleAsync(p => p.Id == productId, cancellationToken);
        var course = await DatabaseContext.Courses.Include(c => c.Products).SingleAsync(c => c.Id == id, cancellationToken);

        product.Courses.Remove(product.Courses.Where(c => c.Id == id).First());
        course.Products.Remove(course.Products.Where(p => p.Id == productId).First());

        DatabaseContext.SaveChanges();

        return course;
    }
}
