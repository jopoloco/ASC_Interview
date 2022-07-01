using BlazorApp1.ViewModels;
using System.Text.Json;

namespace BlazorApp1.Clients;

public class ApiClient : IApiClient
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

    public ApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ProductsVM> GetProducts(SearchQueryParams searchQuery)
    {
        var response = await _httpClient.GetAsync(searchQuery.BuildQueryString("products"));
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Failed");
        }

        var jsonData = await response.Content.ReadAsStringAsync();
        var jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

        var responseObject = JsonSerializer.Deserialize<ProductsVM>(jsonData, jsonSerializerOptions);
        return responseObject;
    }

    public async Task<ProductVM> GetProduct(int id)
    {
        var response = await _httpClient.GetAsync($"products/{id}");
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Failed");
        }

        var jsonData = await response.Content.ReadAsStringAsync();
        var jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

        var responseObject = JsonSerializer.Deserialize<ProductVM>(jsonData, jsonSerializerOptions);
        return responseObject;
    }

    public async Task<ProductVM> UpdateProduct(int id, ProductVM product)
    {
        var response = await _httpClient.PostAsync($"products/{id}/update", JsonContent.Create(product));
        if (!response.IsSuccessStatusCode)
        {
            var msg = await response.Content.ReadAsStringAsync();
            throw new Exception("Failed");
        }

        var jsonData = await response.Content.ReadAsStringAsync();
        var jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

        var responseObject = JsonSerializer.Deserialize<ProductVM>(jsonData, jsonSerializerOptions);
        return responseObject;
    }

    public async Task<ProductVM> AddCourseToProduct(int productId, int courseId)
    {
        var response = await _httpClient.PostAsync($"products/{productId}/add/{courseId}", null);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Failed");
        }

        var jsonData = await response.Content.ReadAsStringAsync();
        var jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

        var responseObject = JsonSerializer.Deserialize<ProductVM>(jsonData, jsonSerializerOptions);
        return responseObject;
    }

    public async Task<ProductVM> RemoveCourseFromProduct(int productId, int courseId)
    {
        var response = await _httpClient.PostAsync($"products/{productId}/remove/{courseId}", null);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Failed");
        }

        var jsonData = await response.Content.ReadAsStringAsync();
        var jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

        var responseObject = JsonSerializer.Deserialize<ProductVM>(jsonData, jsonSerializerOptions);
        return responseObject;
    }

    public async Task<CoursesVM> GetCourses(SearchQueryParams searchQuery)
    {
        var response = await _httpClient.GetAsync(searchQuery.BuildQueryString("courses"));
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Failed");
        }

        var jsonData = await response.Content.ReadAsStringAsync();
        var jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

        var responseObject = JsonSerializer.Deserialize<CoursesVM>(jsonData, jsonSerializerOptions);
        return responseObject;
    }
    public async Task<CourseVM> GetCourse(int id)
    {
        var response = await _httpClient.GetAsync($"courses/{id}");
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Failed");
        }

        var jsonData = await response.Content.ReadAsStringAsync();
        var jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

        var responseObject = JsonSerializer.Deserialize<CourseVM>(jsonData, jsonSerializerOptions);
        return responseObject;
    }

    public async Task<CourseVM> UpdateCourse(int id, CourseVM product)
    {
        var response = await _httpClient.PostAsync($"courses/{id}/update", JsonContent.Create(product));
        if (!response.IsSuccessStatusCode)
        {
            var msg = await response.Content.ReadAsStringAsync();
            throw new Exception("Failed");
        }

        var jsonData = await response.Content.ReadAsStringAsync();
        var jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

        var responseObject = JsonSerializer.Deserialize<CourseVM>(jsonData, jsonSerializerOptions);
        return responseObject;
    }

    public async Task<CourseVM> AddProductToCourse(int courseId, int productId)
    {
        var response = await _httpClient.PostAsync($"courses/{courseId}/add/{productId}", null);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Failed");
        }

        var jsonData = await response.Content.ReadAsStringAsync();
        var jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

        var responseObject = JsonSerializer.Deserialize<CourseVM>(jsonData, jsonSerializerOptions);
        return responseObject;
    }

    public async Task<CourseVM> RemoveProductFromCourse(int courseId, int productId)
    {
        var response = await _httpClient.PostAsync($"courses/{courseId}/remove/{productId}", null);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Failed");
        }

        var jsonData = await response.Content.ReadAsStringAsync();
        var jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

        var responseObject = JsonSerializer.Deserialize<CourseVM>(jsonData, jsonSerializerOptions);
        return responseObject;
    }
}
