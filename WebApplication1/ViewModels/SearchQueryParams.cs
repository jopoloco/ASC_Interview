namespace WebApplication1.ViewModels;

public class SearchQueryParams
{
    public string? SearchText { get; set; }
    public string? SortField { get; set; }
    public bool? Ascending { get; set; }
    public int? Page { get; set; }
    public int? ItemsPerPage { get; set; }
}
