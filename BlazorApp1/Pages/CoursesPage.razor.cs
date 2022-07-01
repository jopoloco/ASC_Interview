using BlazorApp1.Clients;
using BlazorApp1.ViewModels;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BlazorApp1.Pages;

public partial class CoursesPage
{
    [Inject] private IApiClient Client { get; set; }

    private CoursesVM Courses { get; set; }
    private MudTable<CourseVM> table;

    private string searchString = string.Empty;

    protected override void OnInitialized()
    {

    }

    /// <summary>
    /// Here we simulate getting the paged, filtered and ordered data from the server
    /// </summary>
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
