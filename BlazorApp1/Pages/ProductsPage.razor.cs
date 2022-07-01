using BlazorApp1.Clients;
using BlazorApp1.ViewModels;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BlazorApp1.Pages;

public partial class ProductsPage
{
    [Inject] private IApiClient Client { get; set; }

    private ProductsVM Products { get; set; }
    private MudTable<ProductVM> table;

    private string searchString = string.Empty;

    protected override void OnInitialized()
    {

    }

    /// <summary>
    /// Here we simulate getting the paged, filtered and ordered data from the server
    /// </summary>
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
