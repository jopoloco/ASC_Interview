using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorApp1.Shared;

public partial class NavMenu
{
    [Inject] private NavigationManager NavigationManager { get; set; }

    private bool NavOpen { get; set; }

    protected override async Task OnInitializedAsync()
    {
    }

    private void NavToggle(MouseEventArgs e)
    {
        NavOpen = !NavOpen;
    }

    private void CloseNav(MouseEventArgs e)
    {
        NavOpen = false;
    }
}
