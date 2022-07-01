using BlazorApp1.Clients;
using BlazorApp1.ViewModels;
using FluentValidation;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BlazorApp1.Pages;

public partial class ProductEditPage
{
    [Inject] IApiClient Client { get; set; }
    [Inject] ISnackbar Snackbar { get; set; }

    [Parameter]
    public int Id { get; set; }

    private ProductVM product { get; set; }
    private CoursesVM Courses { get; set; }
    private MudTable<CourseVM> table;

    private string searchString = string.Empty;

    MudForm form;

    ProductFluentValidator productValidator = new ProductFluentValidator();

    protected override async Task OnInitializedAsync()
    {
        product = await Client.GetProduct(Id);
    }

    private async Task Submit()
    {
        await form.Validate();

        if (form.IsValid)
        {
            product = await Client.UpdateProduct(product.Id, product);
            Snackbar.Add("Submited!");
        }
    }

    private async Task RemoveCourse(int id)
    {
        product = await Client.RemoveCourseFromProduct(product.Id, id);
        Snackbar.Add("Removed!");
    }

    private async Task AddCourse(int id)
    {
        product = await Client.AddCourseToProduct(product.Id, id);
        Snackbar.Add("Added!");
    }

    private async Task<TableData<CourseVM>> ServerReload(TableState state)
    {
        try
        {
            Courses = await Client.GetCourses(new SearchQueryParams
            {
                Ascending = state.SortDirection == SortDirection.Ascending,
                ItemsPerPage = state.PageSize,
                Page = state.Page,
                SearchText = searchString,
                SortField = state.SortLabel,
            });

            return new TableData<CourseVM>() { TotalItems = Courses.TotalCourses, Items = Courses.PagedCourses };
        }
        catch (Exception ex)
        {
            throw;
        }

    }
}
