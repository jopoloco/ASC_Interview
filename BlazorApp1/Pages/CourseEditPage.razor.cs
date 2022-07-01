using BlazorApp1.Clients;
using BlazorApp1.ViewModels;
using FluentValidation;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BlazorApp1.Pages;

public partial class CourseEditPage
{
    [Inject] IApiClient Client { get; set; }
    [Inject] ISnackbar Snackbar { get; set; }

    [Parameter]
    public int Id { get; set; }

    private CourseVM Course { get; set; }
    private ProductsVM Products { get; set; }
    private MudTable<ProductVM> table;

    private string searchString = string.Empty;

    MudForm form;

    CourseFluentValidator courseValidator = new CourseFluentValidator();

    protected override async Task OnInitializedAsync()
    {
        Course = await Client.GetCourse(Id);
    }

    private async Task Submit()
    {
        await form.Validate();

        if (form.IsValid)
        {
            Course = await Client.UpdateCourse(Course.Id, Course);
            Snackbar.Add("Submited!");
        }
    }

    private async Task RemoveProduct(int id)
    {
        Course = await Client.RemoveProductFromCourse(Course.Id, id);
        Snackbar.Add("Removed!");
    }

    private async Task AddProduct(int id)
    {
        Course = await Client.AddProductToCourse(Course.Id, id);
        Snackbar.Add("Added!");
    }

    private async Task<TableData<ProductVM>> ServerReload(TableState state)
    {
        try
        {
            Products = await Client.GetProducts(new SearchQueryParams
            {
                Ascending = state.SortDirection == SortDirection.Ascending,
                ItemsPerPage = state.PageSize,
                Page = state.Page,
                SearchText = searchString,
                SortField = state.SortLabel,
            });

            return new TableData<ProductVM>() { TotalItems = Products.TotalProducts, Items = Products.PagedProducts };
        }
        catch (Exception ex)
        {
            throw;
        }

    }
}

