using BlazorApp1.ViewModels;

namespace BlazorApp1.Clients;

public interface IApiClient
{
    Task<ProductsVM> GetProducts(SearchQueryParams searchQuery);
    Task<ProductVM> GetProduct(int id);
    Task<ProductVM> UpdateProduct(int id, ProductVM product);
    Task<ProductVM> AddCourseToProduct(int productId, int courseId);
    Task<ProductVM> RemoveCourseFromProduct(int productId, int courseId);
    Task<CoursesVM> GetCourses(SearchQueryParams searchQuery);
    Task<CourseVM> GetCourse(int id);
    Task<CourseVM> UpdateCourse(int id, CourseVM course);
    Task<CourseVM> AddProductToCourse(int courseId, int productId);
    Task<CourseVM> RemoveProductFromCourse(int courseId, int productId);
}
