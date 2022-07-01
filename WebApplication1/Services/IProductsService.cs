using WebApplication1.ViewModels;

namespace WebApplication1.Services;

public interface IProductsService
{
    Task<ProductsVM> GetProducts(SearchQueryParams searchQuery, CancellationToken cancellationToken);
    Task<ProductVM> GetProduct(int id, CancellationToken cancellationToken);
    Task<ProductVM> UpdateProduct(int id, ProductVM updated, CancellationToken cancellationToken);
    Task<ProductVM> AddCourseToProduct(int id, int courseId, CancellationToken cancellationToken);
    Task<ProductVM> RemoveCourseFromProduct(int id, int courseId, CancellationToken cancellationToken);
    Task<CoursesVM> GetCourses(SearchQueryParams searchQuery, CancellationToken cancellationToken);
    Task<CourseVM> GetCourse(int id, CancellationToken cancellationToken);
    Task<CourseVM> UpdateCourse(int id, CourseVM course, CancellationToken cancellationToken);
    Task<CourseVM> AddProductToCourse(int courseId, int productId, CancellationToken cancellationToken);
    Task<CourseVM> RemoveProductFromCourse(int courseId, int productId, CancellationToken cancellationToken);
}
